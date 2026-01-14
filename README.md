🏋️ **FitCore - Platformă Smart Fitness**
FitCore este o soluție software completă pentru gestionarea activității unei săli de fitness. Proiectul conectează o aplicație mobilă (pentru clienți) cu un panou de administrare web, folosind o bază de date centralizată pentru sincronizarea informațiilor în timp real.

Aplicația a fost dezvoltată pentru a simula un scenariu real de producție, unde datele nu sunt stocate local pe telefon, ci sunt gestionate de un server securizat.

🏗️ **Arhitectura Sistemului**
Sistemul este construit pe o arhitectură Client-Server de tip N-Tier, împărțită în trei componente majore:

*Baza de Date Centralizată (SQL Server)*: Aici sunt stocate toate conturile de utilizatori, clasele, antrenorii și rezervările.

*Backend & API (ASP.NET Core)*: Serverul care procesează logica. Acesta securizează accesul la date și le expune prin endpoints REST.

*Mobile Client (.NET MAUI)*: Interfața prin care utilizatorii interacționează cu sistemul.

📱***Funcționalități Cheie***
1. *Aplicația Mobilă (Android)*
Destinată membrilor sălii, aplicația se concentrează pe ușurința în utilizare:

- Autentificare Securizată: Utilizatorii își pot crea conturi și se pot loga. Sistemul folosește token-uri pentru a valida sesiunea.

- Vizualizare Clase: Lista actualizată a claselor (Cardio, Yoga, Pilates etc.) preluată direct din server.

- Rezervări în timp real: Membrii își pot rezerva locul la o clasă cu un singur tap. Aplicația verifică automat dacă mai sunt locuri disponibile.

- Istoric Personal (My Bookings): O secțiune dedicată unde utilizatorul își vede rezervările active și istoricul activității.

- Profil Utilizator: Gestionarea datelor personale.

- Sistem de Notificări: Aplicația folosește notificări locale pentru a reaminti utilizatorului despre rezervările făcute sau pentru confirmarea acțiunilor.

2. **Panoul Web (Administrare)**
Locul de unde se gestionează "business-ul". Orice modificare făcută aici apare instantaneu în aplicația mobilă.

- Dashboard: O privire de ansamblu asupra sălii.

- Management Resurse: Adăugarea, editarea și ștergerea Antrenorilor și a Tipurilor de Clase.

- Gestiune Orar: Administratorul stabilește programul claselor.

🛠️ **Stack Tehnologic**
Proiectul este construit 100% în ecosistemul .NET, demonstrând capacitatea de a lucra Full Stack:

Limbaj: C#

Frontend Mobile: .NET MAUI (Multi-platform App UI) cu pattern-ul MVVM (Model-View-ViewModel) pentru un cod curat și organizat.

Backend: ASP.NET Core Web API + MVC.

Data Access: Entity Framework Core (Code-First/Database-First).

Networking: Comunicare HTTP asincronă (REST/JSON).

📂 **Structura Proiectului**
Soluția este organizată modular pentru a separa responsabilitățile:

- FitCore.Data 🗄️

Conține modelele de date (tabelele: GymClasses, Bookings, Users).

Este biblioteca comună folosită de backend pentru a structura informația.

- FitCore.Web 🌐

Proiectul principal de tip Server.

Conține API Controllers care trimit datele în format JSON către telefon.

- FitCore.Mobile 📱

Aplicația client pentru Android.

Include logica de interfață (Views), logica de prezentare (ViewModels) și serviciile de rețea (ApiService).

🚀 **Rulare și Configurare**
Deoarece aplicația mobilă depinde de server pentru a funcționa (Login, Date), ordinea rulării este importantă:

- Serverul (Backend): Se pornește proiectul FitCore.Web. Acesta va lansa browserul și va deschide conexiunea la baza de date locală.

- Clientul (Mobile): Se rulează FitCore.Mobile pe emulatorul Android.

*Notă tehnică*: Aplicația mobilă este configurată să comunice prin adresa specială 10.0.2.2, care permite emulatorului să "vadă" serverul localhost de pe laptop.
