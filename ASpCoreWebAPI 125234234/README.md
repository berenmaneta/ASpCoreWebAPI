This is a ReadMe file for my test.

Here i would like to comment some of the features present on my solution.

1 - Swagger

The API implements Swashbuckle. SO, it is possible to access all the methods by adding /swagger after the running URL.


2 - Database

To connect with the database just alter the connection string on the appsettings.json file and the one on the DataContext.
I implemented code first. So, there is no need for database creation scripts. Just use migrations and generate the database.
I have provided a script to populate the database with the data needed for the system to work fine. It is on folder 'Database'. 


3 - Pagination 

I implemented pagination using PagedList, but only on backend.
