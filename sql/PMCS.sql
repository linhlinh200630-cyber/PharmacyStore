SELECT TOP (1000) [ProductId]
      ,[Name]
      ,[Price]
      ,[Stock]
      ,[Unit]
      ,[Description]
      ,[ImageUrl]
      ,[CategoryId]
  FROM [PharmacyStoreDb].[dbo].[Products]
  SET XACT_ABORT ON;
BEGIN TRAN;

DECLARE @TopCat INT =
(
    SELECT TOP 1 CategoryId
    FROM dbo.Categories
    WHERE Name LIKE N'%Top bán chạy%' OR Name LIKE N'%Top%'
);
IF @TopCat IS NULL SET @TopCat = 1;

-- Xóa dữ liệu cũ (nếu có)
DELETE FROM dbo.Products
WHERE CategoryId = @TopCat;

-- Thêm mới sản phẩm Top bán chạy
INSERT INTO dbo.Products (Name, Price, Stock, Unit, ImageUrl, CategoryId)
VALUES
(N'Viên ngậm Strepsils Original (2 vỉ x 12)', 18750,  200, N'vỉ',  N'top/strepsils-original.jpg',        @TopCat),
(N'Prospan siro 100ml',                        93000,  150, N'chai',N'top/prospan-100ml.jpg',             @TopCat),
(N'Khẩu trang y tế 3 lớp (50)',                69000,  500, N'hộp', N'top/mask-3lop-xanh-50.jpg',         @TopCat),
(N'Salonpas pain relief patch (5 miếng/hộp)',  45000,  300, N'hộp', N'top/salonpas-5mieng.jpg',           @TopCat),
(N'Eganin Plus (60 viên)',                     205000, 120, N'hộp', N'top/eganin-plus-60.jpg',            @TopCat),
(N'Ensure Gold 800g (vani)',                   840000,  80, N'lon',  N'top/ensure-gold-800g.jpg',         @TopCat),
(N'Condition Ginkgo (60 viên)',                390000, 110, N'hộp', N'top/ginkgo-condition-60.jpg',       @TopCat),
(N'Nature''s Way Complete Daily Multivitamin (100 viên)', 245000, 90, N'hộp', N'top/multivitamin-100.jpg', @TopCat);

COMMIT;

-- Kiểm tra kết quả
SELECT ProductId, Name, Price, Unit, ImageUrl
FROM dbo.Products
WHERE CategoryId = @TopCat
ORDER BY ProductId DESC;


-- HÀNG MỚI VỀ
SET XACT_ABORT ON;
BEGIN TRAN;

-- 1) Lấy (hoặc tạo) Category "Hàng mới về"
DECLARE @NewCat INT =
(
    SELECT TOP 1 CategoryId
    FROM dbo.Categories
    WHERE Name LIKE N'%Hàng mới%' OR Name LIKE N'%Mới về%' OR Name LIKE N'%New%'
);
IF @NewCat IS NULL
BEGIN
    INSERT dbo.Categories(Name) VALUES (N'Hàng mới về');
    SET @NewCat = SCOPE_IDENTITY();
END;

-- 2) Thêm 5 sản phẩm (tránh trùng theo tên)
-- Lưu ý: ImageUrl là đường dẫn TƯƠNG ĐỐI dưới /Content/img/
-- => ảnh bạn đặt ở: /Content/img/spnew/new/<tên-file>.jpg

-- 1. Dầu xả L'ORÉAL PARIS 440ml
IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Name LIKE N'%Dầu xả L''ORÉAL PARIS dưỡng tóc suôn mượt 440ml%')
INSERT dbo.Products (Name, Price, Stock, Unit, ImageUrl, CategoryId)
VALUES (N'Dầu xả L''ORÉAL PARIS dưỡng tóc suôn mượt 440ml', 207200, 120, N'chai',
        N'spnew/new/loreal-dauxa-440.jpg', @NewCat);

-- 2. Ma:nyo Bifida Biome Ampoule Toner 210ml
IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Name LIKE N'%Ma:nyo Bifida Biome Ampoule Toner 210ml%')
INSERT dbo.Products (Name, Price, Stock, Unit, ImageUrl, CategoryId)
VALUES (N'Ma:nyo Bifida Biome Ampoule Toner 210ml', 215000, 120, N'chai',
        N'spnew/new/manyo-toner-210.jpg', @NewCat);

-- 3. Sữa rửa mặt L''Oréal Paris Glycolic Bright 100ml
IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Name LIKE N'%Glycolic Bright 100ml%')
INSERT dbo.Products (Name, Price, Stock, Unit, ImageUrl, CategoryId)
VALUES (N'Sữa rửa mặt L''Oréal Paris Glycolic Bright 100ml', 126650, 120, N'tuýp',
        N'spnew/new/loreal-glycolic-100.jpg', @NewCat);

-- 4. Serum TIA''M Vita B3 Source 40ml
IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Name LIKE N'%TIA''M Vita B3 Source 40ml%')
INSERT dbo.Products (Name, Price, Stock, Unit, ImageUrl, CategoryId)
VALUES (N'Serum TIA''M Vita B3 Source 40ml', 283250, 120, N'hộp',
        N'spnew/new/tiam-b3-40.jpg', @NewCat);

-- 5. Dầu gội L'ORÉAL PARIS 440ml
IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Name LIKE N'%Dầu gội L''ORÉAL PARIS dưỡng tóc suôn mượt 440ml%')
INSERT dbo.Products (Name, Price, Stock, Unit, ImageUrl, CategoryId)
VALUES (N'Dầu gội L''ORÉAL PARIS dưỡng tóc suôn mượt 440ml', 207200, 120, N'chai',
        N'spnew/new/loreal-daugoi-440.jpg', @NewCat);

COMMIT;


UPDATE dbo.Products
SET ImageUrl = 'spnew/loreal-daugoi-440.jpg'
WHERE Name LIKE N'%Dầu gội L''OREAL PARIS%';

UPDATE dbo.Products
SET ImageUrl = 'spnew/loreal-dauxa-440.jpg'
WHERE Name LIKE N'%Dầu xả L''OREAL PARIS%';

UPDATE dbo.Products
SET ImageUrl = 'spnew/loreal-glycolic-100.jpg'
WHERE Name LIKE N'%Sữa rửa mặt L''Oreal%';

UPDATE dbo.Products
SET ImageUrl = 'spnew/manyo-toner-210.jpg'
WHERE Name LIKE N'%Manyo%';

UPDATE dbo.Products
SET ImageUrl = 'spnew/tiam-b3-40.jpg'
WHERE Name LIKE N'%TIA''M Vita B3%';

UPDATE dbo.Products
SET ImageUrl = REPLACE(ImageUrl, 'spnew/new/', 'spnew/')
WHERE ImageUrl LIKE 'spnew/new/%';
SELECT ProductId, Name, ImageUrl
FROM dbo.Products
WHERE ImageUrl LIKE 'spnew/%';

SET XACT_ABORT ON;
BEGIN TRAN;

-- 1) Strepsils Original
UPDATE dbo.Products
SET ImageUrl = 'top/strepsils-original.jpg',
    Price    = 18750
WHERE Name LIKE N'Viên ngậm Strepsils Original%';

-- 2) Ensure Gold 800g (vani)
UPDATE dbo.Products
SET ImageUrl = 'top/ensure-gold-800g.jpg',
    Price    = 840000
WHERE Name LIKE N'Ensure Gold 800g (vani)%';

-- 3) Dầu gội L’ORÉAL PARIS 440ml
UPDATE dbo.Products
SET ImageUrl = 'spnew/loreal-daugoi-440.jpg',
    Price    = 207200
WHERE Name LIKE N'Dầu gội L''ORÉAL PARIS dưỡng tóc suôn mượt 440ml%';

-- 4) Ma:nyo Bifida Biome Ampoule Toner 210ml
UPDATE dbo.Products
SET ImageUrl = 'spnew/manyo-toner-210.jpg',
    Price    = 215000
WHERE Name LIKE N'Ma:nyo Bifida Biome Ampoule Toner 210ml%';

-- 5) Serum TIA’M Vita B3 Source 40ml
UPDATE dbo.Products
SET ImageUrl = 'spnew/tiam-b3-40.jpg',
    Price    = 283250
WHERE Name LIKE N'Serum TIA''M Vita B3 Source 40ml%';

-- Kiểm tra nhanh: 5 món có ảnh từ 2 folder spnew/top
SELECT ProductId, Name, ImageUrl, Price
FROM dbo.Products
WHERE ImageUrl IN (
    'top/strepsils-original.jpg',
    'top/ensure-gold-800g.jpg',
    'spnew/loreal-daugoi-440.jpg',
    'spnew/manyo-toner-210.jpg',
    'spnew/tiam-b3-40.jpg'
)
ORDER BY Name;

COMMIT;
SELECT TOP 5 * FROM Products WHERE ImageUrl LIKE 'spnew%';
UPDATE Products
SET ImageUrl = 'spnew/' + ImageUrl
WHERE ProductId <= 5;
SELECT * FROM Categories;
UPDATE Products
SET ImageUrl = 'spnew/' + ImageUrl
WHERE ProductId IN (SELECT TOP 5 ProductId FROM Products);
SELECT TOP 10 ProductId, Name, CategoryId, ImageUrl
FROM Products;
ALTER TABLE Products
ADD [Usage] NVARCHAR(MAX) NULL,
    [Ingredient] NVARCHAR(MAX) NULL;

UPDATE Products
SET Description = N'Sản phẩm giúp bổ sung ...',
    Usage       = N'Uống 1 viên sau ăn sáng...',
    Ingredient  = N'Ginkgo biloba extract 60 mg...',
    Note        = N'Không dùng cho phụ nữ mang thai...'
WHERE ProductId = 1014;  -- id sản phẩm đang xem
ALTER TABLE Products
ADD CongDung NVARCHAR(MAX) NULL,
    QuyCach NVARCHAR(MAX) NULL;
	ALTER TABLE Products ADD
    CategoryName NVARCHAR(255),
    Usage NVARCHAR(MAX),
    Specification NVARCHAR(255);
	ALTER TABLE Products
ADD QuyCach nvarchar(255) NULL;


	UPDATE Products
SET 
    CongDung = N'- Làm sạch và dưỡng tóc suôn mượt cho tóc khô và xoăn.
- Nhẹ nhàng làm sạch da đầu cho cảm giác thoải mái, nhẹ nhàng.
- Giúp mái tóc bồng bềnh và trông bóng khỏe hơn.',
    QuyCach  = N'440ml'
WHERE ProductId = 1015;
INSERT INTO Categories (Name)
VALUES 
(N'Thuốc'),
(N'Thực phẩm chức năng'),
(N'Dược mỹ phẩm'),
(N'Chăm sóc cá nhân'),
(N'Thiết bị y tế'),
(N'Tiêm chủng');
SELECT ProductId, Name, CategoryId
FROM Products
WHERE ProductId = 1015;
SELECT ProductId, Name 
FROM Products
ORDER BY ProductId DESC;

UPDATE Products
SET 
    CongDung = N'Thoa lên tóc ướt và sạch, tránh xoa trực tiếp lên da đầu.
Massage nhẹ nhàng từ phần thân tóc đến ngọn tóc trong 1–2 phút.
Xả sạch.',
    QuyCach = N'440ml',
    Note = N'Mọi thông tin trên đây chỉ mang tính chất tham khảo. Đọc kỹ hướng dẫn sử dụng trước khi dùng.'
WHERE ProductId = 1015;

UPDATE Products
SET 
    CongDung = N'Sản phẩm dưỡng da, giúp cung cấp độ ẩm cho da, cho da mềm mịn.',
    QuyCach = N'40ml',
    Note = N'Mọi thông tin trên đây chỉ mang tính chất tham khảo. Đọc kỹ hướng dẫn sử dụng trước khi dùng.'
WHERE ProductId = 1014;
UPDATE Products
SET 
    CongDung = N'Sản phẩm dưỡng da giúp dưỡng ẩm, làm sạch da và làm dịu da, cho da luôn mịn màng. Sản phẩm dùng bôi ngoài da.',
    QuyCach = N'210ml',
    Note = N'Mọi thông tin trên đây chỉ mang tính chất tham khảo. Đọc kỹ hướng dẫn sử dụng trước khi dùng.'
WHERE ProductId = 1013;
UPDATE Products
SET 
    CongDung = N'Làm sạch da, giúp lấy đi tế bào chết, hỗ trợ dưỡng trắng mờ thâm nám mang lại cho bạn một làn da khỏe mạnh và tràn đầy sức sống.',
    QuyCach = N'100ml',
    Note = N'Mọi thông tin trên đây chỉ mang tính chất tham khảo. Đọc kỹ hướng dẫn sử dụng trước khi dùng.'
WHERE ProductId = 1013;

UPDATE Products
SET 
    CongDung = N'Sản phẩm dưỡng da giúp dưỡng ẩm, làm sạch da và làm dịu da, cho da luôn mịn màng. Sản phẩm dùng bôi ngoài da.',
    QuyCach = N'210ml',
    Note = N'Mọi thông tin trên đây chỉ mang tính chất tham khảo. Đọc kỹ hướng dẫn sử dụng trước khi dùng.'

	--------------------------------------------------
-- 1. Prospan siro 100ml (Hộp/Chai)
--------------------------------------------------
UPDATE Products
SET CongDung = N'Siro thảo dược giúp giảm ho, long đờm, giảm co thắt phế quản, hỗ trợ làm dịu ho và dễ thở hơn.',
    QuyCach  = N'Chai 100ml'
WHERE ProductId = 1004;


--------------------------------------------------
-- 2. Khẩu trang y tế 3 lớp (Hộp 50 cái)
--------------------------------------------------
UPDATE Products
SET CongDung = N'Sản phẩm giúp ngăn ngừa bụi bẩn, ngăn giọt bắn, ngăn ngừa vi khuẩn gây bệnh qua đường hô hấp, hỗ trợ che chắn và giảm tác động tia UV.',
    QuyCach  = N'Hộp 50 cái'
WHERE ProductId = 1005;


--------------------------------------------------
-- 3. Salonpas pain relief patch (5 miếng/hộp)
--------------------------------------------------
UPDATE Products
SET CongDung = N'Cao dán giảm đau giúp giảm đau vai, đau cổ, đau lưng, đau khớp và đau cơ, hỗ trợ giảm nhức mỏi tại chỗ.',
    QuyCach  = N'Hộp 5 miếng'
WHERE ProductId = 1006;


--------------------------------------------------
-- 4. Eganin Plus (60 viên)
--------------------------------------------------
UPDATE Products
SET CongDung = N'Hỗ trợ giải độc gan và bảo vệ gan, giúp tăng cường chức năng gan, hỗ trợ giảm tác hại của rượu và các chất độc hại đối với gan.',
    QuyCach  = N'Hộp 12 vỉ x 5 viên'
WHERE ProductId = 1007;


--------------------------------------------------
-- 5. Ensure Gold 800g (vani)
--------------------------------------------------
UPDATE Products
SET CongDung = N'Thực phẩm dinh dưỡng giúp cung cấp đầy đủ và cân đối các dưỡng chất. Hỗ trợ người cần phục hồi sức khỏe, người ăn uống kém, người lớn tuổi, giúp tăng cường sức đề kháng và cải thiện chất lượng cuộc sống.',
    QuyCach  = N'Hộp 800g'
WHERE ProductId = 1008;


--------------------------------------------------
-- 6. Condition Ginkgo (60 viên)
--------------------------------------------------
UPDATE Products
SET CongDung = N'Thực phẩm bảo vệ sức khỏe hỗ trợ tuần hoàn máu não, giúp lưu thông máu, hỗ trợ cải thiện trí nhớ và giảm cảm giác chóng mặt, hoa mắt do thiếu máu não.',
    QuyCach  = N'Hộp 60 viên'
WHERE ProductId = 1009;


--------------------------------------------------
-- 7. Nature''s Way Complete Daily Multivitamin (100 viên)
--------------------------------------------------
UPDATE Products
SET CongDung = N'Hỗ trợ bổ sung một số vitamin và khoáng chất cho cơ thể, giúp tăng cường sức khỏe tổng thể, hỗ trợ tăng cường sức đề kháng và giảm mệt mỏi.',
    QuyCach  = N'Hộp 100 viên'
WHERE ProductId = 1010;


SELECT ProductId, Name, CongDung, QuyCach
FROM Products
WHERE ProductId BETWEEN 1004 AND 1010;

UPDATE Products
SET CongDung = N'Điều trị đau họng, giảm đau rát cổ họng, sát khuẩn vùng họng nhờ hoạt chất Dichlorobenzyl Alcohol và Amylmetacresol.',
    QuyCach  = N'2 vỉ x 12 viên'
WHERE ProductId = 1003;
SELECT ProductId, Name, ImageUrl
FROM Products
WHERE ProductId = 1012;
UPDATE Products
SET ImageUrl = 'Content/img/spnew/manyo-toner-210.jpg'
WHERE ProductId = 1012;
SELECT ProductId, Name, CategoryId
FROM Products
WHERE ProductId IN (1011, 1012, 1013, 1014, 1015);
INSERT INTO Categories (Name)
VALUES (N'Thuốc kê đơn');

INSERT INTO Categories (Name)
VALUES (N'Thuốc không kê đơn');
SELECT * FROM Categories ORDER BY CategoryId DESC;
SELECT CategoryId, Name FROM Categories ORDER BY CategoryId;




