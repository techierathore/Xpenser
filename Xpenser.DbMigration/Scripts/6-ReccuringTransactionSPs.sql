CREATE PROCEDURE ReccuringTransactionSelectAll()

BEGIN

SELECT
	`ReccurTransId`, `TransName`, `TransDescription`,`Amount`,
	`TransType`,`Occurance`, `DayOfMonth`,`AppUserId`,
	(SELECT CONCAT(M.`FirstName`,' ',M.`LastName`) FROM AppUser M WHERE M.`AppUserId` = RC.AppUserId ) as Creator
FROM ReccuringTransaction RC;

END;

CREATE PROCEDURE ReccuringTransactionSelect (pReccuringTransactionId bigint(20))

BEGIN

SELECT 
	`ReccurTransId`, `TransName`, `TransDescription`, `Amount`,
	`TransType`,`Occurance`, `DayOfMonth`,`AppUserId`,
FROM ReccuringTransaction WHERE `ReccurTransId` = pReccuringTransactionId;

END;

CREATE PROCEDURE ReccuringTransactionInsert
(
  pTransName varchar(255),
  pTransDescription longtext,
  pAmount double
  pTransType varchar(255),
  pOccurance varchar(255),
  pDayOfMonth tinyint,
  pAppUserId bigint(20),
)
BEGIN

INSERT INTO ReccuringTransaction
(`TransName`, `TransDescription`, `Amount`, `TransType`,`Occurance`, `DayOfMonth`,`AppUserId`)
VALUES
(pTransName, pTransDescription, pAmount, pTransType, pOccurance, pDayOfMonth, pAppUserId);

END;

CREATE PROCEDURE ReccuringTransactionInsert4Id
(
  pTransName varchar(255),
  pTransDescription longtext,
  pAmount double,
  pTransType varchar(255),
  pOccurance varchar(255),
  pDayOfMonth tinyint,
  pAppUserId bigint(20),
  OUT pInsertedId bigint 
)
BEGIN

INSERT INTO ReccuringTransaction
(`TransName`, `TransDescription`, `Amount`, `TransType`,`Occurance`, `DayOfMonth`,`AppUserId`)
VALUES
(pTransName, pTransDescription, pAmount, pTransType, pOccurance, pDayOfMonth, pAppUserId);

SELECT LAST_INSERT_ID() INTO pInsertedId;
END;

CREATE PROCEDURE ReccuringTransactionUpdate
(
  pReccurTransId bigint(20),
  pTransName varchar(255),
  pTransDescription longtext,
  pAmount double,
  pTransType varchar(255),
  pOccurance varchar(255),
  pDayOfMonth tinyint,
  pAppUserId bigint(20),
)
BEGIN

UPDATE ReccuringTransaction
SET  `TransName`= pTransName,`TransDescription` = pTransDescription,
	`Amount` = pAmount,  `TransType` = pTransType,`Occurance` = pOccurance, `DayOfMonth` = pDayOfMonth, `AppUserId` = pAppUserId,
WHERE `ReccurTransId` = pReccurTransId;

END;
