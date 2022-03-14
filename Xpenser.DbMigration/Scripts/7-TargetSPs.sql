CREATE PROCEDURE TargetSelectAll()
BEGIN

SELECT   
  `TargetId`, `TargetTitle`, `TargetDescription`,`CategoryId`,
  `EntryDate`, `TargetDate`,`Amount`,`AppUserId`
	FROM Target;

END;

CREATE PROCEDURE TargetSelect (pTargetId bigint(20))
BEGIN

SELECT 
	`TargetId`, `TargetTitle`, `TargetDescription`,`CategoryId`,
	`EntryDate`, `TargetDate`,`Amount`,`AppUserId`
	FROM Target WHERE `TargetId` = pTargetId;

END;

CREATE PROCEDURE TargetInsert
(
  pTargetTitle varchar(255),
  pTargetDescription longtext,
  pCategoryId bigint(20),
  pEntryDate Datetime(20),
  pTargetDate Datetime(20)
  pAmount Decimal(18,2)
  pAppUserId bigint(20)
)
BEGIN

INSERT INTO Target
( `TargetTitle`, `TargetDescription`,`CategoryId`,`EntryDate`, `TargetDate`,`Amount`,`AppUserId`)
VALUES
(pTargetTitle, pTargetDescription, pCategoryId, pEntryDate, pTargetDate,pAmount,pAppUserId);

END;

CREATE PROCEDURE TargetUpdate
(
  pTargetId bigint(20),
  pTargetTitle varchar(255),
  pTargetDescription longtext,
  pCategoryId bigint(20),
  pEntryDate Datetime(20),
  pTargetDate Datetime(20)
  pAmount Decimal(18,2)
  pAppUserId bigint(20)
)
BEGIN

UPDATE Target
SET  `TargetTitle`= pTargetTitle,`TargetDescription` = pTargetDescription,`CategoryId` = pCategoryId, 
`EntryDate` = pEntryDate, `TargetDate` = pTargetDate, `Amount` = pAmount, `AppUserId` = pAppUserId
WHERE `TargetId` = pTargetId;

END;
