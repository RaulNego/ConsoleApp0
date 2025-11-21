Assignment:
Usage: dotnet run positive|negative|functional

SQL:

1. SELECT * FROM Orders WHERE CustomerId = 'c001'
2. SELECT CustomerId, sum(Amount) as sum FROM Orders Group By CustomerId

Bug Reporting:

Title: Sales basket - total price not updating
Environment (browser, OS, build version, site URL, etc.): UAT environment,URL: buynow-uat.com, Site version: version 4.34 ,OS: linux PC
Steps to reproduce: Logged in as a customer, I added various products to my basket. Then, I added the product with OrderId = 4 named "Monitor" in the basket using the "Add shopping item" button.
Expected result: The purchase price for the basket should raise by x amount, where x is the price of the Monitor.
Actual result: The price for the basket did not update to reflect the newly added product. 
Severity: Critical
Priority: -
Evidence - screenshot, console log, or short textual description of proof
(i usualy dont add more details on such a simple to understand situation)
