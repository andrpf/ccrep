GO  
CREATE TRIGGER Person.uContact2 ON Person.Person  
AFTER INSERT
AS  
    IF ( (SUBSTRING(COLUMNS_UPDATED(), 1, 1) & 20 = 20)   
        AND (SUBSTRING(COLUMNS_UPDATED(), 2, 1) & 1 = 1) )   
    PRINT 'Columns 3, 5 and 9 updated';  
GO  

UPDATE Person.Person   
   SET NameStyle = NameStyle,  
      FirstName=FirstName,  
      EmailPromotion=EmailPromotion;  
GO  