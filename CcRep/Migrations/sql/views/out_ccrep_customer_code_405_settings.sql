﻿CREATE VIEW out.OUT_CCREP_CODE_405_SETTINGS (ID_KEY, CODE_VO, CODE_405, REPLACE_CODE_405, CODE_VO_DESC,
CODE_405_DESC, SECTION, OPERATION_CODE, DIRECTION_FLG, NONRESIDENT_FLG, RESIDENT_FLG, PROPERTY_FLG,
INCLUDE_405_FLG, INCLUDE_406_FLG) 
AS
SELECT ID_KEY, CODE_VO, 
STUFF(CAST((SELECT [text()] = '-'+ ct.TYPE_TOOLING FROM DIC_SETTING_CODE_VO_TO_CODE_TOOLINGS cv_ct 
join DIC_CODE_TOOLING ct on cv_ct.CODE_TOOLING_ID = ct.ID_KEY 
WHERE cv_ct.SETTING_CODE_VO_ID=scv.ID_KEY FOR XML PATH(''), TYPE) AS VARCHAR(100)), 1, 1, '') CODE_405, 
REPLACE405, CODE_VO_DESC, CODE_405_DESC,
SECTION, OPERATION_CODE, DIRECTION_PAY,
NONRESIDENT_FLG = 
case 
when ISSUER_NEREZ = 'true' then 'Y' else 'N'
end,
RESIDENT_FLG = 
case 
when ISSUER_REZ = 'true' then 'Y' else 'N'
end,
PROPERTY_FLG = 
case 
when PROPERTY_FLG = 'true' then 'Y' else 'N'
end,
INCLUDE_405_FLG = 
case 
when INCLUDE_405 = 'true' then 'Y' else 'N'
end,
INCLUDE_406_FLG = 
case 
when INCLUDE_406 = 'true' then 'Y' else 'N'
end
FROM DIC_SETTING_CODE_VO scv