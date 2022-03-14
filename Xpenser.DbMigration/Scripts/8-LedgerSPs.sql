CREATE PROCEDURE LedgerSelectAll()
BEGIN
SELECT	`TransId`,`TransName`,`TransDescription`,`Amount`,
	`TransType`,`AppUserId`,`CategoryId`,`SrcAccId`,
	`DestAccId`,`PicIds` FROM Ledger;
END;

CREATE PROCEDURE LedgerSelect (pTransId bigint(20))
BEGIN
SELECT 	`TransId`,`TransName`,`TransDescription`,`Amount`,
	`TransType`,`AppUserId`,`CategoryId`,`SrcAccId`,
	`DestAccId`,`PicIds`
	FROM Ledger WHERE `TransId` = pTransId;
END;

CREATE PROCEDURE LedgerByUserId (pAppUserId bigint(20))
BEGIN
SELECT 	`TransId`,`TransName`,`TransDescription`,`Amount`,
	`TransType`,`AppUserId`,`CategoryId`,`SrcAccId`,
	`DestAccId`,`PicIds`
	FROM Ledger WHERE `AppUserId` = pAppUserId;
END;

CREATE PROCEDURE `LedgerInsert`(
	IN pTransName varchar(255), IN pTransDescription longtext,
	IN pAmount double,	IN pTransType varchar(255), IN pAppUserId bigint,	IN pCategoryId bigint,
	IN pSrcAccId bigint, IN pDestAccId bigint, IN pPicIds varchar(255),
	OUT pInsertedId bigint  
)
BEGIN
INSERT INTO Ledger
(
	`TransName`,`TransDescription`,`Amount`,
	`TransType`,`AppUserId`,`CategoryId`,`SrcAccId`,
	`DestAccId`,`PicIds`
)
VALUES
(
	pTransName, pTransDescription, pAmount,
	pTransType, pAppUserId,	pCategoryId, pSrcAccId,
	pDestAccId, pPicIds 
);
SELECT LAST_INSERT_ID() INTO pInsertedId;
END;

CREATE PROCEDURE LedgerUpdate
(pTransId bigint, pTransName varchar(255), pTransDescription longtext,
	pAmount double,	pTransType varchar(255), pAppUserId bigint,	pCategoryId bigint,
	pSrcAccId bigint, pDestAccId bigint, pPicIds varchar(255))
BEGIN

UPDATE Ledger
SET  `TransName`= pTransName,`TransDescription` = pTransDescription,`Amount` = pAmount, 
`TransType` = pTransType, `AppUserId` = pAppUserId, `CategoryId` = pCategoryId, `SrcAccId` = pSrcAccId,
 `DestAccId` = pDestAccId, `PicIds` = pPicIds
WHERE `TransId` = pTransId;

END;
