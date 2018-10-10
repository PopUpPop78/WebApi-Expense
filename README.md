# WebApi-Expense

## (a) Run published version by double clicking VBApi.exe

    request url: 
      
      http://localhost:5000/api/expenses, or      
      https://localhost:5001/api/expenses
     
## (b) Open VBApi.sln file in Visual studio run in debug mode
  
    request url: 
      
      http://localhost:62710/api/expenses, or      
      amend the lauchSettings.json in the dependencies
      
## (c) Change the SQL connection string to your connection string in the appsetting.json
    
    "DefaultConnection":"<your connection string>"
    
## (d) Create table matching the model Expense in SQL server (auto-increment Id colum)
