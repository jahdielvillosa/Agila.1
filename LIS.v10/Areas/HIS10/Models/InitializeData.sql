insert into HisEntities([Name],[Remarks],[userGroupId])
values('Matina Animal Center','Testing',1);

insert into HisInstruments([HisEntityId],[Name],[InsCode],[Description],[Specification],[Remarks],[Status])
values(1,'InsA101','101-22','Code Test Machine','V220 A2 P1200W','testin101','ACT'),
		(1,'InsB201','201-23','Acid Test Machine','V220 A2 P1500W','testin221','ACT'),
		(1,'ACER032','AC0-42','Computer Machine','V220 A2 P550W','ACER-PC','ACT');

insert into HisPhysicians([Name],[Remarks],[ContactInfo])
values('Dr Aga Mulach','Testing','09954508517'),
	  ('Dr Juan','Hehehe','09954508517');

insert into HisOrderTypes([Description],[Remarks])
values('BugTest','Testing202'),
('BloodTest','Hehehe101');

insert into HisResultFields([HisOrderTypeId],[AddForType],[Name],[Desc],[SeqNo],[minValue],[maxValue],[Uom])
values (1,0,'Test01','testing',10,'40','60','CM'),
		(1,0,'Test02','testing',20,'40','60','MM'),
		(1,0,'Test03','testing',30,'40','60','mM'),
		(2,0,'A 01','test A',10,'40','60','CM'),
		(2,0,'B 01','test B',20,'40','60','CM'),
		(2,0,'C 01','test C',30,'40','60','CM');


insert into HisRequests([Title],[Description])
values ('Test 101','General Test checkup'),
('Vacc 101','Standard Vaccination'),
('Med 101','General Medication'),
('Vas 101','Vascu Exercises'),
('Ther 101','Theraphy 101')


insert into HisProfiles ([Name],[Remarks],[AccntUserId],[ContactInfo])
values
	('Batch0301A','None','1','09193812657'),
	('Batch0301B','None','2','09193812657'),
	('Batch0301C','None','3','09193812657');

insert into HisIncharges ([Name],[Remarks],[ContactInfo])
values
('Freddie Roach','Trainer/Consultant','09954508517'),
('Chot Reyes','Coach','09954508517'),
('Yeng Guiao','Trainer','09193812657'),
('Cardo Dalisay','Consultant','09279016517');

insert into HisProfileReqs ([HisProfileId],[HisRequestId],[dtRequested],[dtSchedule],[dtPerformed],[Remarks], [HisPhysicianId],[HisInchargeId])
values (1,1,'3/1/2018 10:00:00','3/6/2018 10:36:00','3/6/2018 10:36:00','Initial',1,1),
	   (3,3,'3/6/2018 10:00:00','3/8/2018 9:36:00','3/8/2018 9:36:00','Initial',1,1),
	   (3,1,'3/7/2018 9:00:00','3/8/2018 15:36:00','3/8/2018 15:36:00','Initial',1,1),
	   
	   (3,3,'3/8/2018 7:20:00','3/9/2018 17:36:00','3/9/2018 17:36:00','Initial',1,1),
	   (3,2,'3/9/2018 15:32:00','3/10/2018 10:36:00','3/9/2018 17:36:00','Initial',1,1),
	   (3,4,'3/10/2018 13:09:00','3/12/2018 10:36:00',NULL,'Initial',1,1),
	   
	   (3,3,'3/10/2018 12:12:00','3/12/2018 10:36:00',NULL,'Initial',1,1),
	   (3,1,'3/11/2018 19:19:00','3/14/2018 10:36:00',NULL,'Initial',1,1),
	   (2,2,'3/1/2018 10:00:00','3/18/2018 10:36:00',NULL,'none',1,1);

insert into HisNotifications ([RecType],[Recipient],[Message],[DtSending],[RefId],[RefTable])
values ('Client','0','NOTIFICATION ' + char(13) + ' For: Batch0301A ' + CHAR(13) + ' Request: Vacc 101 none ' + CHAR(13) + ' Scheduled: 3/6/2018 10:36:00 AM ' + CHAR(13) + ' By: Dr Apple Assisted ' + CHAR(13) + ' By: Yeng Guiao ','3/6/2018 10:36:00',1,'HisProfileReqs'),
	   ('Client','0','NOTIFICATION ' + char(13) + ' For: Batch0301A ' + CHAR(13) + ' Request: Vacc 101 none ' + CHAR(13) + ' Scheduled: 3/8/2018 9:36:00 ' + CHAR(13) + ' By: Dr Apple Assisted ' + CHAR(13) + ' By: Yeng Guiao ','3/8/2018 9:36:00',2,'HisProfileReqs'),
	   ('Client','0','NOTIFICATION ' + CHAR(13) + ' For: Batch0301A ' + CHAR(13) + ' Request: Vacc 101 none ' + CHAR(13) + ' Scheduled: 3/8/2018 15:36:00 AM ' + CHAR(13) + ' By: Dr Apple Assisted ' + CHAR(13) + ' By: Yeng Guiao ','3/8/2018 15:36:00',3,'HisProfileReqs'),
	   
	   ('Client','0','NOTIFICATION ' + CHAR(13) + ' For: Batch0301A ' + CHAR(13) + ' Request: Vacc 101 none ' + CHAR(13) + ' Scheduled: 3/9/2018 5:36:00 PM ' + CHAR(13) + ' By: Dr Apple Assisted ' + CHAR(13) + ' By: Yeng Guiao ','3/9/2018 17:36:00',4,'HisProfileReqs'),
	   ('Client','0','NOTIFICATION ' + CHAR(13) + ' For: Batch0301A ' + CHAR(13) + ' Request: Vacc 101 none ' + CHAR(13) + ' Scheduled: 3/10/2018 10:36:00 AM ' + CHAR(13) + ' By: Dr Apple Assisted ' + CHAR(13) + ' By: Yeng Guiao ','3/10/2018 10:36:00',5,'HisProfileReqs'),
	   ('Client','0','NOTIFICATION ' + CHAR(13) + ' For: Batch0301A ' + CHAR(13) + ' Request: Vacc 101 none ' + CHAR(13) + ' Scheduled: 3/12/2018 10:36:00 AM ' + CHAR(13) + ' By: Dr Apple Assisted ' + CHAR(13) + ' By: Yeng Guiao ','3/12/2018 10:36:00',6,'HisProfileReqs'),
	   
	   ('Client','0','NOTIFICATION ' + CHAR(13) + ' For: Batch0301A ' + CHAR(13) + ' Request: Vacc 101 none ' + CHAR(13) + ' Scheduled: 3/12/2018 10:36:00 AM ' + CHAR(13) + ' By: Dr Apple Assisted ' + CHAR(13) + ' By: Yeng Guiao ','3/12/2018 10:36:00',7,'HisProfileReqs'),
	   ('Client','0','NOTIFICATION ' + CHAR(13) + ' For: Batch0301A ' + CHAR(13) + ' Request: Vacc 101 none ' + CHAR(13) + ' Scheduled: 3/14/2018 10:36:00 AM ' + CHAR(13) + ' By: Dr Apple Assisted ' + CHAR(13) + ' By: Yeng Guiao ','3/14/2018 10:36:00',8,'HisProfileReqs'),
	   ('Client','0','NOTIFICATION ' + CHAR(13) + ' For: Batch0301A ' + CHAR(13) + ' Request: Vacc 101 none ' + CHAR(13) + ' Scheduled: 3/18/2018 10:36:00 AM ' + CHAR(13) + ' By: Dr Apple Assisted ' + CHAR(13) + ' By: Yeng Guiao ','3/18/2018 10:36:00',9,'HisProfileReqs');


insert into HisNotificationRecipients ([HisNotificationId],[ContactInfo])
values (1,'09132465877'),(1,'09192959555'),(1,'09279016517'),
	   (2,'09279016517'),(2,'09279016517'),(2,'09279016517'),
	   (3,'09279016517'),(3,'09279016517'),(3,'09279016517'),
	   (4,'09279016517'),(4,'09279016517'),(4,'09279016517'),
	   (5,'09279016517'),(5,'09279016517'),(5,'09279016517'),
	   (6,'09279016517'),(6,'09279016517'),(6,'09279016517'),
	   (7,'09279016517'),(7,'09279016517'),(7,'09279016517'),
	   (8,'09279016517'),(8,'09279016517'),(8,'09279016517'),
	   (9,'09279016517'),(9,'09279016517'),(9,'09279016517');

insert into HisNotificationLogs ([HisNotificationRecipientId],[DtSending],[Status],[Remarks])
values (1,'3/6/2018 10:36:00','Sent','Test'),(2,'3/6/2018 10:36:00','Sent','Test'),(3,'3/6/2018 10:36:00','Sent','Test'),
	   (4,'3/8/2018 9:36:00','Sent','Test'),(5,'3/8/2018 9:36:00','Sent','Test'),(6,'3/8/2018 9:36:00','Sent','Test'),
	   (7,'3/9/2018 15:36:00','Sent','Test'),(8,'3/9/2018 15:36:00','Sent','Test'),(9,'3/9/2018 15:36:00','Sent','Test'),
	   (10,'3/9/2018 17:36:00','Sent','Test'),(11,'3/9/2018 17:36:00','Sent','Test'),(12,'3/9/2018 17:36:00','Sent','Test'),
	   (13,'3/10/2018 17:36:00','Failed','Test'),(13,'3/10/2018 18:36:00','Sent','Test'),(14,'3/10/2018 18:36:00','Sent','Test'),(15,'3/10/2018 18:36:00','Sent','Test'),
	   (16,'3/9/2018 18:36:00','Failed','Test');
