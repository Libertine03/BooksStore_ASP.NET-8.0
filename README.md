# Books shop
## This web application was created using ASP.NET Core 8.0 MVC.
### Architecture: N-Tier.
### Patterns: Repository, Unit of Work.
### Database: PostgreSQL.

This web application allows you to _add_, _edit_ and _delete_ **categories** of books and the **books** themselves. All changes are saved to _database tables_.

The possibility of _registration and authorization_ has also been added. You can perform all of the above functions only if you have **administrator rights**. You can select a user role during registration.

## Main page
The main page of the application displays all existing books in the database. 
In the header of the page there are Home, Privacy and **Content Management** tabs. The last tab is only available if you are an _administrator_. Using this tab, you can go to the category and movie editor.
![Main page](https://github.com/Libertine03/BooksStore_ASP.NET-8.0/blob/master/GitImages/MainPage.png?raw=true)


## Category editor
The category editor page allows you to add, delete, and edit categories.
![Category editor](https://github.com/Libertine03/BooksStore_ASP.NET-8.0/blob/master/GitImages/CategoriesPage.png?raw=true)

## Product editor
On the product editor page there is a table of all books. Data can be sorted by each column. It is also possible to search for a book and display the desired number of books on a page.
![Products editor](https://github.com/Libertine03/BooksStore_ASP.NET-8.0/blob/master/GitImages/ProductsPage.png?raw=true)


## Editing a movie
The screenshots below show the registration and authorization forms.
![Registration](https://github.com/Libertine03/BooksStore_ASP.NET-8.0/blob/master/GitImages/Registration.png?raw=true)
![Authorization](https://github.com/Libertine03/BooksStore_ASP.NET-8.0/blob/master/GitImages/Authorization.png?raw=true)