PRINT 'Populating Users Table...';
INSERT INTO Users (UserName, Email, Password, Lock) VALUES
('trungpham', 'trung.pham@tdtu.edu.vn', 'pass123', 0), -- Hashing needed in real apps
('janedoe', 'jane.doe@example.com', 'password', 0),
('bobsmith', 'bob.smith@sample.org', 'bobpass', 0),
('alicej', 'alice.j@mail.com', 'alice1', 1), -- Locked account
('testadmin', 'admin@tdtu.edu.vn', 'adminpass', 0),
('guestuser', 'guest@example.net', 'guest', 0),
('miket', 'mike.t@sample.com', 'mikepass', 0),
('sarahk', 'sarah.k@mail.org', 'sarah123', 0),
('davidl', 'david.l@tdtu.edu.vn', 'davidpw', 0),
('emilyw', 'emily.w@example.com', 'emilypass', 0),
('chrisp', 'chris.p@sample.org', 'chris1', 0),
('laurab', 'laura.b@mail.net', 'laurapass', 1), -- Locked account
('kevinm', 'kevin.m@tdtu.edu.vn', 'kevin123', 0),
('oliviar', 'olivia.r@example.com', 'oliviapw', 0),
('samuelg', 'sam.g@sample.net', 'sampass', 0);
GO

-- =============================================
-- Populate Item Table (15 Rows)
-- =============================================
PRINT 'Populating Item Table...';
INSERT INTO Items (ItemName, Size) VALUES
('Laptop Pro X', '15 inch'),
('Wireless Mouse G5', 'Standard'),
('Mechanical Keyboard K7', 'Full Size'),
('Ultrawide Monitor', '34 inch'),
('USB-C Cable', '2m'),
('Docking Station', 'Universal'),
('Webcam HD 1080p', 'Standard'),
('Noise Cancelling Headphones', 'Over-ear'),
('SSD Drive', '1TB'),
('Graphics Card RTX 4070', 'Standard'),
('RAM Module', '16GB DDR4'),
('Laptop Stand', 'Adjustable'),
('Ergonomic Chair', 'Large'),
('Desk Lamp LED', 'Standard'),
('Power Strip', '10 Outlet');
GO

-- =============================================
-- Populate Agent Table (15 Rows)
-- =============================================
PRINT 'Populating Agent Table...';
INSERT INTO Agents (AgentName, Address) VALUES
('Global Tech Supplies', '100 Tech Plaza'),
('Office Solutions Hub', '200 Business Blvd'),
('Component Central', '300 Circuit Ave'),
('Gadget Galaxy', '400 Innovation Dr'),
('Ergo Comfort Zone', '500 Wellness Way'),
('PC Builders Paradise', '600 Silicon St'),
('AV Network Pros', '700 Media Ln'),
('Cable Connectors Co.', '800 Link Rd'),
('Digital Display Direct', '900 Pixel Pkwy'),
('Power Up Electronics', '10 Power Ct'),
('Workspace Wonders', '11 Office Park'),
('Compute Components', '12 Logic Ln'),
('Sound & Vision Ltd.', '13 Audio Ave'),
('Accessory Arena', '14 Gadget Grv'),
('The Peripheral Place', '15 Input Is');
GO

-- =============================================
-- Populate Order Table (15 Rows)
-- =============================================
PRINT 'Populating Order Table...';
-- Ensure AgentIDs used below exist in the Agent table (IDs 1 through 15 should exist)
INSERT INTO [Orders] (OrderDate, AgentID) VALUES
('2023-10-01 09:15:00', 1),  -- AgentID 1
('2023-10-02 11:05:00', 3),  -- AgentID 3
('2023-10-03 15:30:00', 2),  -- AgentID 2
('2023-10-04 08:00:00', 5),  -- AgentID 5
('2023-10-05 13:45:00', 1),  -- AgentID 1
('2023-10-06 10:20:00', 4),  -- AgentID 4
('2023-10-07 16:00:00', 6),  -- AgentID 6
('2023-10-08 11:55:00', 7),  -- AgentID 7
('2023-10-09 14:10:00', 2),  -- AgentID 2
('2023-10-10 09:30:00', 8),  -- AgentID 8
('2023-10-11 12:00:00', 9),  -- AgentID 9
('2023-10-12 17:05:00', 10), -- AgentID 10
('2023-10-13 10:50:00', 5),  -- AgentID 5
('2023-10-14 13:25:00', 11), -- AgentID 11
('2023-10-15 15:50:00', 12); -- AgentID 12
GO

-- =============================================
-- Populate OrderDetail Table (30+ Rows)
-- =============================================
PRINT 'Populating OrderDetail Table...';
-- Ensure OrderIDs (1-15) and ItemIDs (1-15) used below exist from previous steps

-- Order 1 Details
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (1, 1, 2, 1199.99); -- Laptop Pro X
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (1, 2, 2, 24.95);   -- Wireless Mouse G5
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (1, 7, 1, 59.99);   -- Webcam HD 1080p

-- Order 2 Details
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (2, 3, 5, 89.50);   -- Mechanical Keyboard K7
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (2, 9, 3, 99.99);   -- SSD Drive 1TB

-- Order 3 Details
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (3, 4, 1, 449.00); -- Ultrawide Monitor
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (3, 5, 10, 4.75);  -- USB-C Cable 2m
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (3, 15, 2, 29.99); -- Power Strip

-- Order 4 Details
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (4, 13, 4, 179.95); -- Ergonomic Chair Large
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (4, 14, 4, 35.50); -- Desk Lamp LED

-- Order 5 Details
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (5, 1, 1, 1210.00); -- Laptop Pro X
INSERT INTO OrderDetail (OrderID, ItemID, Quantity, UnitAmount) VALUES (5, 6, 1, 149.99); -- Docking Station Universal

-- Order 6 Details
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (6, 10, 2, 599.00); -- Graphics Card RTX 4070
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (6, 11, 4, 65.00);  -- RAM Module 16GB DDR4

-- Order 7 Details
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (7, 8, 3, 199.99); -- Noise Cancelling Headphones
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (7, 12, 3, 45.00); -- Laptop Stand Adjustable

-- Order 8 Details
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (8, 7, 10, 55.00); -- Webcam HD 1080p

-- Order 9 Details
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (9, 2, 20, 22.50); -- Wireless Mouse G5
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (9, 3, 15, 85.00); -- Mechanical Keyboard K7

-- Order 10 Details
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (10, 9, 5, 95.00);  -- SSD Drive 1TB
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (10, 11, 8, 62.50); -- RAM Module 16GB DDR4

-- Order 11 Details
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (11, 4, 2, 430.00); -- Ultrawide Monitor

-- Order 12 Details
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (12, 15, 5, 28.00); -- Power Strip
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (12, 5, 30, 4.50);  -- USB-C Cable 2m

-- Order 13 Details
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (13, 13, 1, 185.00); -- Ergonomic Chair Large
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (13, 14, 1, 38.00); -- Desk Lamp LED
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (13, 12, 1, 48.00); -- Laptop Stand Adjustable

-- Order 14 Details
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (14, 6, 2, 145.00); -- Docking Station Universal
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (14, 8, 1, 210.00); -- Noise Cancelling Headphones

-- Order 15 Details
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (15, 10, 1, 610.00); -- Graphics Card RTX 4070
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (15, 11, 2, 68.00); -- RAM Module 16GB DDR4
INSERT INTO OrderDetails (OrderID, ItemID, Quantity, UnitAmount) VALUES (15, 9, 1, 105.00); -- SSD Drive 1TB
GO

PRINT 'Finished populating all tables.';

