using Microsoft.EntityFrameworkCore;
using proj1.Controllers;
using proj1.DB;
using proj1.DB.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace proj1.Services {
    public class ProductService : BackgroundService {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ApiController> _logger;
        private readonly IConfiguration _configuration;
        private decimal _priceUpperBound;

        public ProductService(IServiceScopeFactory scopeFactory, ILogger<ApiController> logger, IConfiguration configuration) {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _configuration = configuration;
            _priceUpperBound = int.Parse(_configuration["PriceUpperBound"]);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            while (!stoppingToken.IsCancellationRequested) {
                using (var scope = _scopeFactory.CreateScope()) {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    try {
                        await Chunker(dbContext);
                    } catch (Exception ex) {
                        await using var connection = dbContext.Database.GetDbConnection();
                        await connection.CloseAsync();

                        _logger.LogError(ex.Message);
                    }
                }
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);

            }
        }


        public async Task Chunker(ApplicationDbContext dbContext) {
            try {

                var products = await dbContext
                    .Products
                    .Where(p => p.Status == ProductStatus.Active)
                    .ToListAsync();


                products = products
                    .OrderByDescending(p => p.UnitPrice)
                    .ToList();


                var groups = new List<GroupedProduct>();

                while (products.Last().Quantity != 0) {
                    var groupBudget = _priceUpperBound;
                    var groupNumber = await getNextSequenceValueAsync();
                    for (int i = 0; i < products.Count; i++) {
                        if (products[i].UnitPrice > groupBudget || products[i].Quantity == 0) {
                            continue;
                        }
                        var takenProductCount = Math.Min((int)(groupBudget / products[i].UnitPrice), products[i].Quantity);

                        groupBudget -= takenProductCount * products[i].UnitPrice;
                        products[i].Quantity -= takenProductCount;

                        groups.Add(new GroupedProduct() {
                            GroupName = $"Группа {groupNumber}",
                            ProductId = products[i].Id,
                            Quantity = takenProductCount
                        });
                    }
                }
                products.ForEach(p => p.Status = ProductStatus.Inactive);

                dbContext.GroupedProducts.AddRange(groups);
                 
                await dbContext.SaveChangesAsync();


            } catch (Exception ex) {
                _logger.LogError(ex.Message);
            }



        }




        #region NextSequenceValue
        public async Task<int> getNextSequenceValueAsync() {


            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await using var connection = dbContext.Database.GetDbConnection();
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = "SELECT NEXTVAL('addgroupedproductssequence')";

            var result = await command.ExecuteScalarAsync();
            await connection.CloseAsync();



            return (int)(long)result;


        }
        #endregion
    }
}
