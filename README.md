🦷 DentalApp

DentalApp este o aplicație web creată pentru gestionarea programărilor într-un cabinet stomatologic.
Proiectul a fost realizat în ASP.NET Core 8, folosind Entity Framework Core, principiile Clean Architecture și pattern-ul CQRS implementat cu MediatR.


🚀 Funcționalități principale
- Creare cont utilizator și validare prin e-mail.
- Autentificare și autorizare bazată pe roluri (Client și Admin).
- Programare la doctor prin alegerea procedurii și a intervalului orar disponibil.
- Gestionarea doctorilor și procedurilor din zona de administrare.
- Notificare prin e-mail cu o zi înainte de programare.
- Raport statistic filtrabil după doctor, procedură și număr de programări.
- Interfață Razor Pages cu layout modern.


🧱 Arhitectură
Proiectul este structurat pe layere, conform principiilor Clean Architecture:
src/
- ├── Application     → logica de business, CQRS, DTO-uri, validări
- ├── Domain          → entitățile principale (Doctor, Procedure, Appointment, etc.)
- ├── Infrastructure  → acces la baza de date, e-mail, Identity, repository-uri
- └── Web             → interfața web (Razor Pages, controllers, endpoints)


Tehnologii folosite:
- ASP.NET Core 8
- Entity Framework Core (Code First)
- MediatR
- AutoMapper
- Identity (autentificare cu roluri)
- SQL Server LocalDB


⚙️ Cum rulezi aplicația
1️⃣ Build
- dotnet build


2️⃣ Rulează aplicația
- cd src/Web
- dotnet watch run


Accesează:
➡️ https://localhost:5001

Aplicația se va reîncărca automat la fiecare modificare.


🧩 Baza de date
- Proiectul folosește EF Core Code First.
- Pentru a aplica migrațiile:
- dotnet ef database update


Dacă faci modificări în model:
- dotnet ef migrations add [NumeMigratie]
- dotnet ef database update


🧪 Testare
Momentan nu sunt incluse teste unitare, însă arhitectura este pregătită pentru extindere în acest sens.


💡 Despre proiect
Acest proiect a fost dezvoltat ca parte dintr-un exercițiu de evaluare tehnică (homework).
Scopul a fost exersarea implementării principiilor Clean Architecture, configurarea autentificării cu roluri și lucrul cu Entity Framework Core într-o aplicație complet funcțională.


👨‍💻 Autor
- Girnet Andrei
- Backend Developer – .NET
- 📧 [aagirnet@gmail.com]
- 📅 Octombrie 2025

<img width="1918" height="1030" alt="image" src="https://github.com/user-attachments/assets/56e0c8de-8b68-4f77-b83f-52c82346ebcf" />
