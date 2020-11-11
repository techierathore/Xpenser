CREATE PROCEDURE `GetUserToken`(
	pUserId bigint(20), 
	pLoginToken mediumtext
)
BEGIN

SELECT `LoginId`,`UserId`,`LoginDate`,`LoginToken`,
	`TokenStatus`,`ExipryDate`, `IssueDate`
FROM UserLogin WHERE `UserId` = pUserId AND `LoginToken`=pLoginToken;

END;


CREATE PROCEDURE `UserLoginsInsert`(
	pUserId bigint(20), pLoginDate date,
	pLoginToken mediumtext, pTokenStatus varchar(60),
	pExipryDate date,pIssueDate date
)
BEGIN

INSERT INTO UserLogin
(
	`UserId`,`LoginDate`,`LoginToken`,`TokenStatus`,
	`ExipryDate`,`IssueDate`
)
VALUES
(	pUserId,pLoginDate,pLoginToken,pTokenStatus,
	pExipryDate,pIssueDate
);

END;