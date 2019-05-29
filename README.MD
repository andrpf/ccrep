# CcRep

Система отчетности ВК (CCREP). 

Приложение предназначено для подготовки обязательной отчетности в ЦБ РФ по формам 0409405 и 0409406

### Языки
- C#
- JavaScript
- HTML/CSS
- Microsoft SQL Server Transact SQL (2016+)

### Технологии разработки
- ASP.NET MVC 5
- Entity Framework CodeFirst
- Jquery
- Bootstrap 4
- CDC

### Информация для установки проекта
- Вся структура таблиц хранится в виде файлов миграций .NET;
- В проекте используется NPM для управления некоторыми JS-пакетами;
- Для логирования изменениний в некоторых таблицах используется система CDC (Change Data Captuere), после развертования необходимо выполнить набор инструкций для активации отслеживания:
```
-- Остановить SQL Server Agent (Если будут ошибки)
EXEC xp_servicecontrol N'STOP',N'SQLServerAGENT' 
GO

-- Запуск агента
EXEC xp_servicecontrol N'START',N'SQLServerAGENT' 
GO

-- Включить отслеживание для базы данных
USE ccrep  
GO  
EXEC sys.sp_cdc_enable_db  
GO  

-- =============  
-- Добавить таблицу для отслеживания CREATE, INSERT, UPDATE
-- =============  
USE ccrep  
GO  
EXEC sys.sp_cdc_enable_table  
@source_schema = N'dbo',  
@source_name   = N'{TABLE_NAME}',  
@role_name     = N'public',  
@supports_net_changes = 1,
-- @captured_column_list = N'id,name,family'
GO  

-- Изменить время устаревания логов до 180 дней
sp_cdc_change_job @job_type='cleanup', @retention=259200

```