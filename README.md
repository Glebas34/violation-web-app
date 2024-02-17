# ViolationWebApp
## О проекте
ViolationWebApp - это Web-приложение для учёта нарушения правил дорожного движения и оплаты штрафа. 
##  Запуск проекта
1. Откройте проект в Visual Studio.
2. Создайте локальную базу данных PostgreSQL на своём компьютере.
3. Перейдите обратно в отркытый проект и откройте файл appsettings.json.
4. В ConnectionStrings измените строку PostgreSql, исходя из свойств вашей базы данных:
  ```"PostgreSql": "Host=localhost; Database=your_database; Username=your_username; Password=your_password"```.
5. Откройте консоль диспетчера пакетов.
6. Пропишите в ней команду ```Update-Database```.
7. Откройте консоль PowerShell. В файле Seed.cs можете изменить данные или добавить свои перед сидированием(опционально).
8. Пропишите следующие команды в консоль PowerShell:
   ```cd ViolationWebApplication```,
   ```dotnet run seeddata```.
После проделанных выше действий приложение готово к работе.
## Документация
### Технологии
+ ASP.NET Core MVC
+ PostgreSQL
+ HTML
+ CSS

### Функционал
+	возможность добавления в базу данных информации о нарушении, машине и её владельце у пользователя с ролью admin;
+	оплата штрафов с последующим удалением нарушения из базы данных;
+	удаление машины из базы данных при оплате всех штрафов;
+	создание аккаунта;
+	вход в аккаунт;
+	выход из аккаунта;
+	разделение пользователей по ролям(admin и user);
+	возможность удалить любое нарушение напрямую(без оплаты) из базы данных у пользователей с ролью admin.

### Структура проекта приложения
Для проекта был выбран шаблон MVC(Model-View-Controller).
+ Модели: AppUser.cs, Car.cs, Owner.cs, Violation.cs.
+ Контроллеры: AccountController.cs, HomeController.cs, ViolationController.cs
+ Представления: в папке ViolationWebApplication/Views хранятся представления для контроллеров.

Классы в папке ViolationWebApplication/Repository и AppDbContext нужны для работы с базой данных.

