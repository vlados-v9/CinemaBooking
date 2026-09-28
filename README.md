# CinemaBooking Web API

RESTful API для системи онлайн-бронювання квитків у кінотеатрі, розроблений на платформах **.NET** та **ASP.NET Core**.

---

## 🚀 Особливості

- **Swagger / OpenAPI**: Вбудована документація API для зручного тестування ендпоінтів.
- **Шар БД та Домену**: Чіткий поділ на шари (`DbLayer`, `Domain`, `Api`).
- **Глобальна обробка помилок**: Використання кастомного `ExceptionHandlingMiddleware`.

---

## 🛠 Попередні вимоги (Prerequisites)

Перед початком переконайтеся, що у вас встановлено:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) (або новіша версія)
- [Git](https://git-scm.com/)
- Будь-яке зручне середовище розробки (IDE):
  - **Visual Studio 2022**
  - **JetBrains Rider**
  - **Visual Studio Code** (із розширенням C# Dev Kit)

---

## 📥 Клонування проєкту

Скопіюйте репозиторій на свій комп'ютер:

```bash
git clone https://github.com/your-username/CinemaBooking.git
cd CinemaBooking
```

---

## ⚙️ Налаштування

База даних є в проєкті `CinemaBooking.Api` під назвою `CinemaDb.db`, так як я використовував SQLite. Клас `ConnectionStringToDb` повинен сам побудуватти шлях до цього файлу.
`CinemaBookingContext` вже буде використовувати дані з `CinemaDb.db`.

---

## 🏁 Запуск проєкту

Завдяки налаштованому файлу `launchSettings.json`, при запуску проєкту в середовищі `Development` автоматично відкривається сторінка документації **Swagger UI**.

### Спосіб 1: Через IDE (Visual Studio / Rider / VS Code)

1. Відкрийте рішення (`CinemaBooking.sln`) у вашому IDE.
2. Переконайтеся, що обрано профіль запуску **`https`** або **`http`**.
3. Натисніть кнопку **Run** / **Start Debugging** (або клавішу `F5`).
4. Браузер автоматично відкриється з інтерфейсом Swagger.

### Спосіб 2: Через .NET CLI (Термінал)

Перейдіть у папку з веб-проєктом (де знаходиться файл `Program.cs`) і виконайте команду:

```bash
# Запуск з профілем HTTPS
dotnet run --launch-profile https

# Або запуск з профілем HTTP
dotnet run --launch-profile http
```

---

## 🔗 Доступні адреси (Endpoints)

Після запуску додатку Swagger UI доступний за адресами:

- **HTTPS**: [https://localhost:7268/swagger](https://localhost:7268/swagger)
- **HTTP**: [http://localhost:5030/swagger](http://localhost:5030/swagger)

---

## 🏗 Структура проєкту

Проєкт побудований із дотриманням принципів Багатошарової Архітектури (Layered Architecture):

- `CinemaBooking.Api` — Веб-API, контролери, Middleware та точка входу (`Program.cs`).
- `CinemaBooking.Domain` — Сервіси бізнес-логіки та доменні моделі.
- `CinemaBooking.DbLayer` — Взаємодія з базою даних, контекст та репозиторії.