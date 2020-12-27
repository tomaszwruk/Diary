DECLARE @var0 nvarchar(128)
SELECT @var0 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.Students')
AND col_name(parent_object_id, parent_column_id) = 'LastName';
IF @var0 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[Students] DROP CONSTRAINT [' + @var0 + ']')
ALTER TABLE [dbo].[Students] ALTER COLUMN [LastName] [nvarchar](100) NOT NULL
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202012271820347_SetLastNameMaxLength100RequiredInStudentsTable', N'Diary.ApplicationDBContext',  0x1F8B0800000000000400E55ADB6EDC36107D2FD07F10F4D416CECA765E5A633781B3B60BA3B11D789DA06F06579A5DB3952855A48C5D14FDB23EF493FA0B2575A578D165B3B6DB0601022FC9391C0ECF0CC919FDFDE75FD3B79B28741E21A5382633F76872E83A40FC38C0643D7333B67AF5BDFBF6CDD75F4DCF8368E37CAAC6BD16E3B824A133F781B1E4C4F3A8FF0011A29308FB694CE3159BF871E4A120F68E0F0F7FF08E8E3CE0102EC7729CE96D46188E20FFC17FCE63E243C232145EC50184B46CE73D8B1CD5B94611D004F93073CF304AB7AE731A62C4275F40B8721D4448CC10E3AA9D7CA4B060694CD68B8437A0F06E9B001FB742218552E59366F850ED0F8F85F65E235841F91965713412F0E875690E4F15DFC9A86E6D2E6EB0736E58B615ABCE8D36737F4CE32C711D75A69379988A51A5412785E127677C0C2674924B1D3879DF41BDEF9C1EE2DF8133CF4296A5302390B1148507CE876C1962FF27D8DEC5BF0299912C0C65B5B862BCAFD5C09B3EA4710229DBDEC2AA54F632701DAF2DE7A982B5982453ACE492B0D7C7AE73CD2747CB10EA5D9756BD60710A3F02811431083E20C620E59B761D13D06656E611FF5733718A7107719D2BB4790F64CD1E66EE31F7880BBC81A06A2827FF483077272EC3D20C0CCA29935EA347BCCE7555A65FB02C00C2A8EBDC42980FA00F38293C605276DE979B7D91C6D16D1C365245C7FD1D4AD7C0F8226253EF22CE525FD168EA3584EAA45909359A68A5DC9743B54BB15C6EBB3EBA5DE094B21ECE1D1DEE8774CAD4EFD14BCD3C8FA3A820B97566FEE7A099BB273AF5197EC40C433DD5BB98BB0422A355CE7DA79F1843DDBCF4E0DD7DBCF262B38F571160A83AB7BC91ACCD41A7E8BBAF3DBFD1A8DDA3851DA5FBB3E24E81353AEC14625F4ED41972C0719BC0481A9BCFA8D1DEA0C264CB5FC01F0FD377767E1E8B55C7B2907C088B4F298D7D9C6BD2D6B074EEF6B2CE49E0747A7A112AA518C10326E72F4E3863F9EC33F73BCD5436CCDA551BCC32DEB4118F5C95EB37E40C4260E088D02A2EC273447D14E8BBC46D12B45BB87B402A4E4514F29700E50E8709D37D09131F2728ECD25B111AE88242A91A5EED3983048898A96B0F86CC5B1F14FAE4F51C8AA5FA0C33F5242675134CA1B98D0D36CE3774A802EE7086D94E83B1B4B5AEB670326E21C6ED036975C42702281F7CF64E74C2C6743DE50FC6F2A8A0E5D5415D87005F0093B7911F888D67B7FD4433435BBCB9C66B00B5157A20EA335943A8F6460190CCA629525D27A431C6FB86CAD9BEA0542B2D2F59637E5F1892502ABBABD1A4BDB8010B5703BEBEF22E5F19E22D92D6F55E752CDDE21F032CD8B1FAEAE8A9BDA2C9A9784552A54ABE7896ECCBF40A258950BE912C5B9C45918A99BF5A8C4F58440586E75343DEA2D6B69E895F67D01A945EF15008207F219D218696485C06E641A40D33C6008B775553B6DC5CDFB5CAE5AAE1E2EF42A4B8681ADDA5B1DD055F8E78E2E42B0395DFBA5C9E0343214A0D97C2791C6611B19F6A76E9E27527CB172D3AC2D45314D70E2FCD24DA71DF36F020F3D78C1FBF0116B71DB00556C9A7D904E9852F8348CDC3B19A27BB0CD5B40E476A9EE03252D33A1C497E63CB5872FB70B4FAFA244359EF542F46DC2AE08FE76D79788FA7AD4DF069585BBC1265F9A2653882F44A9461A4E61158CD53B185D5343F3F33DA67B2891ECDE5C34C82A6DFBCD71D371363FE46BDA1E82619449412CCFC8691E61EAD96F5F53648AD0A659C5EEADD49DF4AED0AA50EA989545FA5942BD3B4BCBEF457B5B4FB4C31C475B8EE8F38107799C59632882662C064F15B380F71CE906AC0152278059415F929F7F8F0E858A992FD7B2A561EA54138A46CF5EC19362C4C3A325D25A7CAC9234AFD07946AA5A14ECC3D54592E49009B99FBBBF3C70B19ADB7DC31D2AA5AFDC368DABC0EF199E58DBD01ABD58B0AF89B086DBE95D176AB502CF1786E2AD509DCCD1D2173E25CFE7C5F8A1D3837290F2D27CE21A7D59E286C4DD8BF3883479A564E97EF22AF25CB77025153E5FD202377CA9C311CB4275DF7057DB4FD18EFDF4969A67144AF054750DDBA953BCCDF48EEC1D7762D2B9429C067CDF96B99BFDDEA183B950E6C299AA7AA15FCE7CA03C6778296B11C50167822CAB42393AEC6D3D2C7FA50DF3B7F3A5F7BA343EE0BF0C8F4B0DBCDF5FF974C1AB3B3CF4EA5EE67F5DEB9642EE2E9797E4B16AEFDF98CAD3857BCAA676EB08CF9661707ACA5B06429DC75D5ED4CE8D6D28DB9AAD751D433A15BCA4B1670FB12947EFB54D6E53C6D8DB1B5AD52B9644045D15448DB7B15B12EFDF515132D1938C3D9AA2778FB6B88A6252B3DFB5E7855ADEC5DB839C7B7DB7E3DF1D247D44EF5041F0F81D2C7ED3C0E53BC6E20C4A7EE04FC56F0ABC75C92555CC56145A36A88F2F0B80286021E194F538657C867BCDB074A73B67C4261C6879C474B082EC94DC6928CF12543B40C5B1F9F8A58DE357F5E206EEB3CBD49F2AFA8F6B104AE26E64B801BF22EC36150EB7D6178F75820C42151669EC45E3291815A6F6B24FDDB371B5069BEFA6CBB8328093918BD210BF408BBE8F691C27B58237F5BE569ED20FD1BD136FBF40CA3758A225A6234F2FC27E770106DDEFC0370B6E579E3310000 , N'6.2.0-61023')

