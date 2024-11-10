
# Product Organizer
Проект на основе .NET 8 и PostgreSQL для управления товарами и их группирование. Этот проект использует Entity Framework Core и Swagger.

## Требования

- **.NET SDK**: версия 8.0
- **PostgreSQL**: версия 16.0

## Настройка и запуск проекта

### 1. Установка PostgreSQL

1. Установите PostgreSQL версии 16.0, если он еще не установлен. Инструкция: [PostgreSQL](https://www.postgresql.org/download/).
2. Запустите PostgreSQL сервер и создайте базу данных `dbProductOrganizer`.
   
   ```sql
   CREATE DATABASE dbProductOrganizer;
3.  Установите пароль для базы данных.

### 2. Настройка `appsettings.json`

В корневой директории проекта найдите файл `appsettings.json` и откройте его. Внесите следующие изменения:
 

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=dbProductOrganizer;Username=postgres;Password={pwd}"
  }
}
```
Замените `{pwd}` на ваш фактический пароль для базы данных PostgreSQL.

### 3. Запуск миграций (если используются Entity Framework миграции)

1.  Откройте терминал в корневой директории проекта.
    
2.  Выполните команду для применения миграций, чтобы создать все необходимые таблицы в базе данных:
   
    `dotnet ef database update` 
    

### 4. Запуск проекта

1.  Запустите и дождитесь, пока приложение запустится. Вы должны увидеть в терминале сообщение о том, что приложение запущено, и URL Swagger.
    

### 5. Открытие Swagger UI

1.  Откройте браузер и перейдите по адресу, который отображается в терминале после запуска проекта. Обычно это `http://localhost:5267/swagger` или `https://localhost:7031/swagger` (если используется HTTPS).
2.  В Swagger UI вы сможете протестировать API и проверить его работу.
