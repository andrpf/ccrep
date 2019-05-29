sp_configure 'show advanced options', 1;
RECONFIGURE;
GO
sp_configure 'Ad Hoc Distributed Queries', 1;
RECONFIGURE;
GO

use ccrep;
go
SELECT * INTO VK_ARCHIVE_TMP
FROM OPENROWSET('Microsoft.ACE.OLEDB.12.0','Excel 12.0; Database={0}', [DataAccess$]);

