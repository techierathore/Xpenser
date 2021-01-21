CREATE PROCEDURE CategorySelectAll()

BEGIN

SELECT   
  `CategoryId`, `CategoryName`, `CategoryDescription`,
  `ParentId`,`AppUserId`, `IconPicId`,
  (SELECT CONCAT(M.`FirstName`,' ',M.`LastName`) FROM AppUser M WHERE M.`AppUserId` = C.AppUserId ) as Creator,
  (SELECT P.`CategoryName` FROM Category P WHERE P.`CategoryId` = C.ParentId ) as ParentName
FROM Category C;

END;

CREATE PROCEDURE CategorySelect (pCategoryId bigint(20))

BEGIN

SELECT 
	`CategoryId`, `CategoryName`, `CategoryDescription`,
  `ParentId`,`AppUserId`, `IconPicId`
FROM Category WHERE `CategoryId` = pCategoryId;

END;

CREATE PROCEDURE CategoryInsert
(
  pCategoryName varchar(255),
  pCategoryDescription longtext,
  pParentId bigint(20),
  pAppUserId bigint(20),
  pIconPicId bigint(20)
)
BEGIN

INSERT INTO Category
(`CategoryName`, `CategoryDescription`, `ParentId`,`AppUserId`, `IconPicId`)
VALUES
(pCategoryName, pCategoryDescription, pParentId, pAppUserId, pIconPicId);

END;

CREATE PROCEDURE CategoryInsert4Id
(
  pCategoryName varchar(255),
  pCategoryDescription longtext,
  pParentId bigint(20),
  pAppUserId bigint(20),
  pIconPicId bigint(20),
  OUT pInsertedId bigint 
)
BEGIN

INSERT INTO Category
(`CategoryName`, `CategoryDescription`, `ParentId`,`AppUserId`, `IconPicId`)
VALUES
(pCategoryName, pCategoryDescription, pParentId, pAppUserId, pIconPicId);

SELECT LAST_INSERT_ID() INTO pInsertedId;
END;

CREATE PROCEDURE CategoryUpdate
(
  pCategoryId bigint(20),
  pCategoryName varchar(255),
  pCategoryDescription longtext,
  pParentId bigint(20),
  pAppUserId bigint(20),
  pIconPicId bigint(20) 
)
BEGIN

UPDATE Category
SET  `CategoryName`= pCategoryName,`CategoryDescription` = pCategoryDescription,
	`ParentId` = pParentId, `AppUserId` = pAppUserId, `IconPicId` = pIconPicId
WHERE `CategoryId` = pCategoryId;

END;
