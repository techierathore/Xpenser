CREATE PROCEDURE `AppUserSelectAll`()
BEGIN

SELECT 
  `AppUserId`, `FirstName`, `LastName`, `EmailID`, `PasswordHash`, `MobileNo`,
  `Verified`, `Role`, `ProfilePicId`
FROM AppUser;

END;

CREATE PROCEDURE `AppUserSelect`(pAppUserId long)
BEGIN
SELECT 
  `AppUserId`, `FirstName`, `LastName`, `EmailID`, `PasswordHash`, `MobileNo`,
  `Verified`, `Role`, `ProfilePicId`
FROM AppUser  WHERE `AppUserId` = pAppUserId;
END;

CREATE PROCEDURE `AppUserInsert`(
	IN pFirstName varchar(255),
	IN pLastName varchar(255),
	IN pEmailID varchar(355),
	IN pPasswordHash varchar(35),
	IN pMobileNo varchar(35),
	IN pVerified tinyint(1),
	IN pRole varchar(55),
	OUT pInsertedId bigint  
)
BEGIN

INSERT INTO AppUser
(
  `FirstName`, `LastName`, `EmailID`, `PasswordHash`, `MobileNo`,
  `Verified`, `Role`
)
VALUES
(
	pFirstName, pLastName, pEmailID, pPasswordHash, pMobileNo,
	pVerified, pRole
);
SELECT LAST_INSERT_ID() INTO pInsertedId;
END;

CREATE PROCEDURE `AppUserUpdate`(
  pAppUserId bigint,
  pFirstName varchar(255),
  pLastName varchar(255),
  pEmailID varchar(355),
  pPasswordHash varchar(35),
  pMobileNo varchar(35),
  pVerified tinyint(1),
  pRole varchar(55)
)
BEGIN
UPDATE AppUser
SET 
	`FirstName`= pFirstName, `LastName`=pLastName,
	`EmailID` = pEmailID,`PasswordHash` = pPasswordHash,
	`MobileNo` = pMobileNo,`Verified` = pVerified,
	`Role` = pRole
WHERE `AppUserId` = pAppUserId;

END;

CREATE PROCEDURE `ValidateLogin`(pEmail varchar(255), pPasswordHash varchar(255))
BEGIN
SELECT 
  `AppUserId`, `FirstName`, `LastName`, `EmailID`, `PasswordHash`, `MobileNo`,
  `Verified`, `Role`, `ProfilePicId`
FROM AppUser  
WHERE `EmailID` = pEmail AND `PasswordHash` = pPasswordHash;
END;

CREATE PROCEDURE `AppUserByEmail`(pEmailID varchar(355))
BEGIN
SELECT 
  `AppUserId`, `FirstName`, `LastName`, `EmailID`, `PasswordHash`, `MobileNo`,
  `Verified`, `Role`, `ProfilePicId`
FROM AppUser  WHERE `EmailID` = pEmailID;
END;

CREATE PROCEDURE `AppUserByMobile`(pMobileNo varchar(355))
BEGIN
SELECT 
  `AppUserId`, `FirstName`, `LastName`, `EmailID`, `PasswordHash`, `MobileNo`,
  `Verified`, `Role`, `ProfilePicId`
FROM AppUser  WHERE `MobileNo` = pMobileNo;
END;