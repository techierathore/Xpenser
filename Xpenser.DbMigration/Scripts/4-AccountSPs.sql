CREATE PROCEDURE AccountSelectAll()
BEGIN
SELECT `AccountId`,`AcccountName`,`AcNumber`,`OpenBal`,
    `AcType`,`StartDate`,`AppUserId`, `IconPicId`
FROM Account;
END;

CREATE PROCEDURE AccountsByUserId (pAppUserId bigint(20))

BEGIN
SELECT 	`AccountId`,`AcccountName`,`AcNumber`,`OpenBal`,
	`AcType`,`StartDate`,`AppUserId`, `IconPicId`
FROM Account WHERE `AppUserId` = pAppUserId;
END;

CREATE PROCEDURE AccountSelect (pAccountId bigint(20))
BEGIN
SELECT	`AccountId`,`AcccountName`,`AcNumber`,`OpenBal`,
	`AcType`,`StartDate`,`AppUserId`, `IconPicId`
FROM Account WHERE `AccountId` = pAccountId;
END;

CREATE PROCEDURE AccountInsert
(
  pAcccountName varchar(455),
  pAcNumber varchar(255),
  pOpenBal double,
  pAcType varchar(255),
  pStartDate datetime,
  pAppUserId bigint(20),
  pIconPicId bigint(20)
)
BEGIN

INSERT INTO Account
(
	`AcccountName`,`AcNumber`,`OpenBal`,
	`AcType`,`StartDate`,`AppUserId`, `IconPicId`
)
VALUES
(
	pAcccountName, pAcNumber, pOpenBal, pAcType,
	pStartDate, pAppUserId,pIconPicId
);
END;

CREATE PROCEDURE AccountInsert4Id
(
  pAcccountName varchar(455),
  pAcNumber varchar(255),
  pOpenBal double,
  pAcType varchar(255),
  pStartDate datetime,
  pAppUserId bigint(20),
  pIconPicId bigint(20),
	OUT pInsertedId bigint 
)
BEGIN

INSERT INTO Account
(
	`AcccountName`,`AcNumber`,`OpenBal`,
	`AcType`,`StartDate`,`AppUserId`, `IconPicId`
)
VALUES
(
	pAcccountName, pAcNumber, pOpenBal, pAcType,
	pStartDate, pAppUserId,pIconPicId
);
SELECT LAST_INSERT_ID() INTO pInsertedId;
END;

CREATE PROCEDURE AccountUpdate
(
  pAccountId bigint(20),
  pAcccountName varchar(455),
  pAcNumber varchar(255),
  pOpenBal double,
  pAcType varchar(255),
  pStartDate datetime,
  pAppUserId bigint(20),
  pIconPicId bigint(20) 
)
BEGIN

UPDATE Account
SET  `AcccountName`= pAcccountName,`AcNumber` = pAcNumber,`OpenBal` = pOpenBal,
	 `AcType` = pAcType,`StartDate` = pStartDate,`AppUserId` = pAppUserId,
	 `IconPicId` = pIconPicId
WHERE `AccountId` = pAccountId;

END;