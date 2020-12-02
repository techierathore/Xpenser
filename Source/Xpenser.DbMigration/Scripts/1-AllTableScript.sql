CREATE TABLE `AppUser` (
  `AppUserId` bigint(20) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(255) NOT NULL,
  `LastName` varchar(255) NOT NULL,
  `EmailID` varchar(355) NOT NULL,
  `PasswordHash` varchar(35) NOT NULL,
  `MobileNo` varchar(35) NOT NULL,
  `Verified` bit(1) DEFAULT NULL,
  `Role` varchar(55) NOT NULL,
  `ProfilePicId` bigint(20) DEFAULT NULL,
	PRIMARY KEY (`AppUserId`)
);

CREATE TABLE `Account` (
	`AccountId` bigint NOT NULL AUTO_INCREMENT,
	`AcccountName` varchar(455) NOT NULL,
	`AcNumber` varchar(255) NOT NULL,
	`OpenBal` double NOT NULL,
	`AcType` varchar(255) NOT NULL,
	`StartDate` DATETIME NOT NULL,
	`AppUserId` bigint NOT NULL,
	`IconPicId` bigint,
	PRIMARY KEY (`AccountId`)
);

CREATE TABLE `Category` (
	`CategoryId` bigint NOT NULL AUTO_INCREMENT,
	`CategoryName` varchar(255) NOT NULL,
	`CategoryDescription` longtext,
	`ParentId` bigint NOT NULL,
	`AppUserId` bigint(20) DEFAULT NULL,
	`IconPicId` bigint,
	PRIMARY KEY (`CategoryId`)
);

CREATE TABLE `ReccuringTransaction` (
	`ReccurTransId` bigint NOT NULL AUTO_INCREMENT,
	`TransName` varchar(255) NOT NULL,
	`TransDescription` longtext NOT NULL,
	`Amount` double NOT NULL,
	`TransType` varchar(255) NOT NULL,
	`Occurance` varchar(255) NOT NULL,
	`DayOfMonth` tinyint NOT NULL,
	`AppUserId` bigint NOT NULL,
	PRIMARY KEY (`ReccurTransId`)
);

CREATE TABLE `Target` (
	`TargetId` bigint NOT NULL AUTO_INCREMENT,
	`TargetTitle` varchar(255) NOT NULL,
	`TargetDescription` bigint,
	`CategoryId` bigint NOT NULL,
	`EntryDate` DATETIME NOT NULL,
	`TargetDate` DATETIME NOT NULL,
	`Amount` double NOT NULL,
	`AppUserId` bigint NOT NULL,
	PRIMARY KEY (`TargetId`)
);

CREATE TABLE `UserLogin` (
	`LoginId` bigint NOT NULL AUTO_INCREMENT,
	`UserId` bigint NOT NULL,
	`LoginDate` DATETIME NOT NULL,
	`LoginToken` mediumtext NOT NULL,
	`TokenStatus` varchar(55) NOT NULL,
	`ExipryDate` DATETIME NOT NULL,
	`IssueDate` DATETIME NOT NULL,
	PRIMARY KEY (`LoginId`)
);

CREATE TABLE `Ledger` (
	`TransId` bigint NOT NULL AUTO_INCREMENT,
	`TransName` varchar(255) NOT NULL,
	`TransDescription` longtext,
	`Amount` double NOT NULL,
	`TransType` varchar(255) NOT NULL,
	`AppUserId` bigint NOT NULL,
	`CategoryId` bigint NOT NULL,
	`AccountId` bigint NOT NULL,
	`PicIds` varchar(255) NOT NULL,
	PRIMARY KEY (`TransId`)
);

CREATE TABLE `ImagesNDoc` (
	`ImgDocId` bigint NOT NULL AUTO_INCREMENT,
	`ImgDocOrigName` varchar(355) NOT NULL,
	`ImgDocName` varchar(355) NOT NULL,
	`ImgDocPath` varchar(355) NOT NULL,
	`ContentType` varchar(155) NOT NULL,
	`ImgDocType` varchar(155) NOT NULL,
	`BaseRecordId` bigint NOT NULL,
	PRIMARY KEY (`ImgDocId`)
);



