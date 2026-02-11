# Mission06_Atkinson – The Joel Hilton Film Collection

ASP.NET Core MVC web app for IS 413 Mission #6 (Hilton), built to help Joel Hilton track his movie collection [file:2].

## Tech Stack

- ASP.NET Core MVC (.NET 10.0)
- Entity Framework Core (Model First)
- SQLite
- Custom CSS (no external CSS framework)

---

## How to Run the Project

1. **Clone or download** this repository.
2. Open the solution in **Visual Studio 2022**.
3. Ensure the following NuGet packages are installed:
   - `Microsoft.EntityFrameworkCore.Sqlite`
   - `Microsoft.EntityFrameworkCore.Tools`
   - `Microsoft.EntityFrameworkCore.Design`
4. Check `appsettings.json` for the connection string:

   ```json
   "ConnectionStrings": {
     "MovieCollectionContext": "Data Source=MovieCollection.sqlite"
   }
   ```

5. Open **Tools → NuGet Package Manager → Package Manager Console** and run:

   ```powershell
   Add-Migration InitialCreate
   Update-Database
   ```

6. Press **F5** (or Ctrl+F5) to run the app.

---

## App Features

### Navigation

Shared navigation menu on all pages:

- **Home** – The Joel Hilton Film Collection (title and headshot)
- **Movie Collection** – List of all movies
- **Add Movie** – Form to add a new movie
- **Get to Know Joel** – Info and external links [file:2]

### Pages

#### Home

- Title: **“The Joel Hilton Film Collection”** [file:2]
- Displays Joel’s headshot (`JoelHiltonHeadshot.jpg`)
- Buttons to browse the collection and add a new movie

#### Get to Know Joel

- Link to **Quick Wits Comedy**  
  `https://www.qwcomedy.com/` [file:2]
- Link to **Baconsale**  
  `https://baconsale.com/` [file:2]
- Image `BaconsaleKate.jpg` that links to Baconsale when clicked [file:2]

#### Movie Collection

- Displays all movies from the database in a table.
- Shows:
  - Title
  - Category
  - Year (start–end if applicable)
  - Director(s)
  - Rating
  - Edited (Yes/No)
  - Lent To
  - Notes
- Includes:
  - Search by title or director
  - Filter by category
  - Filter by rating
  - Sorting (title, year, category, rating)
  - Edit/Delete buttons for each movie

#### Add / Edit Movie

Form fields (matching the spreadsheet columns) [file:3]:

- **Category** – required, dropdown from Categories table.
- **Title** – required.
- **Start Year** – required (int).
- **End Year** – optional (int, for ranges like TV seasons).
- **Director(s)** – required, comma-separated; stored via `Directors` table and junction table.
- **Rating** – required, dropdown (G, PG, PG-13, R, plus any other ratings present in the sheet such as NR, UR, TV ratings) [file:3].
- **Edited** – optional, checkbox (true/false), default unchecked; corresponds to the “Edited” column [file:2][file:3].
- **Lent To** – optional; corresponds to the “Lent To” column [file:3].
- **Notes** – optional, max 25 characters; corresponds to the “Notes” column [file:2][file:3].

Model validation enforces the required fields and notes length [file:2].

---

## Database Design

Using **Model First** with EF Core and SQLite, as required by the assignment [file:2].

### Tables

- `Movies`
  - `MovieId` (PK)
  - `CategoryId` (FK)
  - `Title`
  - `StartYear`
  - `EndYear` (nullable)
  - `Rating`
  - `Edited` (bool)
  - `LentTo` (nullable)
  - `Notes` (nullable, max 25 chars)

- `Categories`
  - `CategoryId` (PK)
  - `CategoryName` (values drawn from the spreadsheet’s Category column such as Comedy, Drama, Family, etc.) [file:3]

- `Directors`
  - `DirectorId` (PK)
  - `DirectorName` (names drawn from the Director column) [file:3]

- `MovieDirectors` (junction table)
  - `MovieDirectorId` (PK)
  - `MovieId` (FK)
  - `DirectorId` (FK)

### Normalization

- Categories are normalized to a separate table instead of repeating strings in Movies [file:3].
- Directors are normalized into their own table, with a many-to-many link via `MovieDirectors` to handle multiple directors per movie [file:3].
- Optional fields (“Edited”, “Lent To”, “Notes”) remain in Movies but marked nullable/optional, matching the assignment [file:2].

---
