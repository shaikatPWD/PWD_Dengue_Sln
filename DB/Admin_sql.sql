SET IDENTITY_INSERT [dbo].[Country] ON 

INSERT [dbo].[Country] ([Id], [Code], [Name], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (1, N'BD', N'Bangladesh', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Country] OFF
SET IDENTITY_INSERT [dbo].[Division] ON 

INSERT [dbo].[Division] ([Id], [CountryId], [DivisionNameEng], [DivisionNameBng], [BBSCode], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (2, 1, N'Dhaka', N'Dhaka', NULL, NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Division] OFF
SET IDENTITY_INSERT [dbo].[District] ON 

INSERT [dbo].[District] ([Id], [DivisionId], [DistrictNameEng], [DistrictNameBng], [BBSCode], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (1, 2, N'Dhaka', N'Dhaka', NULL, NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[District] OFF
SET IDENTITY_INSERT [dbo].[Thana] ON 

INSERT [dbo].[Thana] ([Id], [ThanaNameEng], [ThanaNameBng], [BBSCode], [DistrictId], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (1, N'Gulshan', NULL, NULL, 1, NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Thana] OFF
SET IDENTITY_INSERT [dbo].[Address] ON 

INSERT [dbo].[Address] ([Id], [PhoneNo], [CellPhoneNo], [Email], [AddressLine1], [AddressLine2], [AddressLine3], [PostalCode], [ThanaId], [DistrictId], [DivisionId], [CountryId], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (1, N'01195172224', N'01195172224', N'sh@gmail.com', N'add1', N'add2', N'add3', NULL, 1, 1, 2, 1, NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Address] OFF
SET IDENTITY_INSERT [dbo].[OperatorType] ON 

INSERT [dbo].[OperatorType] ([Id], [Name], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (1, N'Bus Operator', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[OperatorType] ([Id], [Name], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (2, N'Launch Operator', CAST(N'2019-01-16T21:26:28.523' AS DateTime), 1, NULL, NULL, 1)
INSERT [dbo].[OperatorType] ([Id], [Name], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (3, N'Flights Operator', CAST(N'2019-01-16T21:27:29.757' AS DateTime), 1, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[OperatorType] OFF
SET IDENTITY_INSERT [dbo].[Operator] ON 

INSERT [dbo].[Operator] ([Id], [Name], [Description], [Address], [Rating], [IconUrl], [LogoUrl], [Website], [AffiliateUrl], [Mobile], [OperatorTypeId], [Remarks], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (2, N'Shyamoli', N'Shyamoli', N'1', 5, NULL, NULL, NULL, NULL, N'01195172224', 1, NULL, NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Operator] OFF
SET IDENTITY_INSERT [dbo].[RootLocation] ON 

INSERT [dbo].[RootLocation] ([Id], [Name], [Description], [Remarks], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (1, N'Dhaka', N'Dhaka', N'Dhaka', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[RootLocation] ([Id], [Name], [Description], [Remarks], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (2, N'Chittang', N'Chittang', N'Chittang', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[RootLocation] ([Id], [Name], [Description], [Remarks], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (3, N'Sylhet', N'Sylhet', N'Sylhet', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[RootLocation] ([Id], [Name], [Description], [Remarks], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (4, N'Khulna', N'Khulna', N'Khulna', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[RootLocation] OFF
SET IDENTITY_INSERT [dbo].[RootLocationType] ON 

INSERT [dbo].[RootLocationType] ([Id], [Name], [Remarks], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (1, N'Start Point', N'Start Point', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[RootLocationType] ([Id], [Name], [Remarks], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (2, N'Pickup Point', N'Pickup Point', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[RootLocationType] ([Id], [Name], [Remarks], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (3, N'Interval Point', N'Interval Point', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[RootLocationType] ([Id], [Name], [Remarks], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (4, N'Drop Off Point', N'Drop Off Point', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[RootLocationType] ([Id], [Name], [Remarks], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (5, N'End Point', N'End Point', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[RootLocationType] OFF
SET IDENTITY_INSERT [dbo].[RoutePoint] ON 

INSERT [dbo].[RoutePoint] ([Id], [RouteId], [RootLocationId], [OperatorId], [Latitude], [Longitude], [RootLocationTypeId], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (6, 1, 1, 2, NULL, NULL, 1, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[RoutePoint] ([Id], [RouteId], [RootLocationId], [OperatorId], [Latitude], [Longitude], [RootLocationTypeId], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (7, 1, 2, 2, NULL, NULL, 5, NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[RoutePoint] OFF
SET IDENTITY_INSERT [dbo].[RouteType] ON 

INSERT [dbo].[RouteType] ([Id], [Name], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (1, N'Bus', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[RouteType] ([Id], [Name], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (2, N'Fliet', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[RouteType] OFF
SET IDENTITY_INSERT [dbo].[Route] ON 

INSERT [dbo].[Route] ([Id], [Name], [StartPointId], [EndPointId], [RouteTypeId], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (2, N'Dhaka to Chittang', 6, 7, 1, NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Route] OFF
SET IDENTITY_INSERT [dbo].[FacilityType] ON 

INSERT [dbo].[FacilityType] ([Id], [Name], [Remarks], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (1, N'Entertainment', NULL, CAST(N'2019-01-18T16:46:13.747' AS DateTime), 1, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[FacilityType] OFF
SET IDENTITY_INSERT [dbo].[Facilities] ON 

INSERT [dbo].[Facilities] ([Id], [Name], [FacilityTypeId], [Remarks], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (11, N'WiFi', 1, N'WiFi', CAST(N'2019-01-18T20:39:04.643' AS DateTime), 1, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Facilities] OFF
SET IDENTITY_INSERT [dbo].[VehicleTypes] ON 

INSERT [dbo].[VehicleTypes] ([Id], [Name], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (1, N'AC', CAST(N'2019-01-17T01:47:34.690' AS DateTime), 1, NULL, NULL, 1)
INSERT [dbo].[VehicleTypes] ([Id], [Name], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (2, N'Non AC', CAST(N'2019-01-17T01:47:48.193' AS DateTime), 1, NULL, NULL, 1)
INSERT [dbo].[VehicleTypes] ([Id], [Name], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (3, N'Sleeper', CAST(N'2019-01-17T12:48:03.777' AS DateTime), 1, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[VehicleTypes] OFF
SET IDENTITY_INSERT [dbo].[VehicleModel] ON 

INSERT [dbo].[VehicleModel] ([Id], [Name], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (1, N'Scania', CAST(N'2019-01-18T15:51:22.127' AS DateTime), 1, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[VehicleModel] OFF
SET IDENTITY_INSERT [dbo].[VehicleMenufacturer] ON 

INSERT [dbo].[VehicleMenufacturer] ([Id], [Name], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (1, N'Man', CAST(N'2019-01-18T16:14:28.003' AS DateTime), 1, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[VehicleMenufacturer] OFF
SET IDENTITY_INSERT [dbo].[Vehicle] ON 

INSERT [dbo].[Vehicle] ([Id], [OperatorId], [Name], [RegNo], [IssueDate], [ExpireyDate], [VehicleTypeId], [VehicleModelId], [VehicleMenufacturerId], [CesisNo], [Owner], [Rating], [Remarks], [CreateDate], [CreatedBy], [EditDate], [EditedBy], [Status]) VALUES (3, 2, NULL, N'654654', CAST(N'2001-02-20T00:00:00.000' AS DateTime), CAST(N'2021-02-20T00:00:00.000' AS DateTime), 1, 1, 1, N'97879', N'fadfasd', 5, N'dfadfa', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Vehicle] OFF
