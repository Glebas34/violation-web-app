# ViolationWebApp
## О проекте
ViolationWebApp - это Web-приложение для учёта нарушения правил дорожного движения и оплаты штрафа. 
##  Запуск проекта
1. Скачайте и установите Docker: https://www.docker.com/get-started/
2. Запустите Docker Desktop.
3. Откройте терминал( например, PowerShell или Bash).
4. Перейдите в директорию с проектом.
5. Наберите в терминале команду: `docker compose up`.
6. После запуска контейнеров приложение будет доступно по ссылке: https://localhost:8081

После проделанных выше действий приложение готово к работе.
## Документация
### Технологии
+ ASP.NET Core MVC
+ PostgreSQL
+ HTML
+ CSS
+ Entity Framework Core

### Функционал
+	возможность добавления в базу данных информации о нарушении, машине и её владельце у пользователя с ролью admin;
+	оплата штрафов с последующим удалением нарушения из базы данных;
+	удаление машины из базы данных при оплате всех штрафов;
+	создание аккаунта;
+	вход в аккаунт;
+	выход из аккаунта;
+	разделение пользователей по ролям(admin и user);
+	возможность удалить любое нарушение напрямую(без оплаты) из базы данных у пользователей с ролью admin.

### Структура проекта
Для проекта был выбран шаблон MVC(Model-View-Controller).
+ Модели: AppUser.cs, Car.cs, Violation.cs.
+ Контроллеры: AccountController.cs, HomeController.cs, ViolationController.cs, CarController.cs.
+ Представления: в папке ViolationWebApplication/Views хранятся представления для контроллеров.

Классы в папке ViolationWebApplication/Repository и AppDbContext нужны для работы с базой данных.

