﻿CREATE VIEW out.OUT_CCREP_LOG (ID_KEY, DATE_LOG, STATUS, START_DATE, END_DATE) AS
SELECT ID_KEY,
CONVERT(VARCHAR(20),CAST(DATE_LOG AS DATETIME),103), STATUS,
CONVERT(VARCHAR(20),CAST(START_DATE AS DATETIME),103) + ' '
+ CONVERT(VARCHAR(20),CAST(START_DATE AS DATETIME),24),
CONVERT(VARCHAR(20),CAST(END_DATE AS DATETIME),103) + ' '
+ CONVERT(VARCHAR(20),CAST(END_DATE AS DATETIME),24)
FROM out.OUT_LOG

