using System;
using System.Collections.Generic;
using CcRep.Models._dc;
using CcRep.Models.vk;


namespace CcRep.Models._db
{
    public class CcRepTestContextDBInitializer : CcRepContextDBInitializer
    {

        protected override void Seed(CcRepContext context)
        {
            base.Seed(context);

            //  TestingApiFilters test_filters = new TestingApiFilters(context);
            //  test_filters.Test_1();

            /*
            CcRepUser user = new CcRepUser {
                FullName = "Петр Иванов", UserName = "test_user_2",
                Email = "test_user_2@test.com" // , SecurityStamp = Guid.NewGuid().ToString()
            };
            */

            /*
            var BrAccessClaim = new CcRepUserClaim { ClaimType = "showAllBranches", ClaimValue = "True" };
            var FlAccessClaim = new CcRepUserClaim { ClaimType = "FlAccess", ClaimValue = "*" };
            var PdAccessClaim = new CcRepUserClaim { ClaimType = "PdAccess", ClaimValue = "*" };

            // добавляем claim пользователю
            user.Claims.Add(BrAccessClaim);
            user.Claims.Add(FlAccessClaim);
            user.Claims.Add(PdAccessClaim);
            */

            /*
            var defaultUsers = new List<CcRepUser>() { user };
           ((DbSet<CcRepUser>)context.Users).AddRange(defaultUsers);
           */

            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;

            var startDate = new DateTime(year, month, 1).AddMonths(-1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var Rep1 = new HeaderRep()
            {
                BeginDate = startDate,
                EndDate = endDate,
                CreateDate = startDate,
                UserLastEditedId = null,
                //   UserLastEdited = defaultUsers[0]
            };

            var dataHeaderReps = new List<HeaderRep>()
            {
                    Rep1
            };

            context.HeaderReps.AddRange(dataHeaderReps);

            var createDate = startDate;

            var dataAddInfReps = new List<AddInfRep>()
            {
                new AddInfRep { Form="405", Section = "1.1", Status = "INCLUDE", IdRep = Rep1.Id,
                                IrbPaymentRK = 1, PaymentRK = 1, CNum = "1", NumDVK = "2", Source = "LOADED", NumJD = "10",
                                HeaderRep = Rep1, SupdocFl = "N", Author = "Петров", CBAccount = "1111122222333334441",
                                DateCreate = createDate, DateGetDoc = createDate, DateRequest = DateTime.Now },
                new AddInfRep { Form="406", Section = "1.2", Status = "INCLUDE", IdRep = Rep1.Id,
                                IrbPaymentRK = 2, PaymentRK = 2, CNum = "2", NumDVK = "4", Source = "LOADED", NumJD = "11",
                                HeaderRep = Rep1, SupdocFl = "N", Author = "Иванов", CBAccount = "1111122222333334442",
                                DateCreate = createDate, DateGetDoc = createDate },
                new AddInfRep { Form="405", Section = "1.1", Status = "DUPLICATE", IdRep = Rep1.Id,
                                IrbPaymentRK = 3, PaymentRK = 3, CNum = "5", NumDVK = "6", Source = "LOADED", NumJD = "12",
                                HeaderRep = Rep1, SupdocFl = "Y", Author = "Сидоров", CBAccount = "1111122222333334443",
                                DateCreate = DateTime.Now, DateGetDoc = DateTime.Now, DateRequest = DateTime.Now },
                new AddInfRep { Form="406", Section = "1.2", Status = "NOINFORM", IdRep = Rep1.Id,
                                IrbPaymentRK = 4, PaymentRK = 4, CNum = "3", NumDVK = "5", Source = "LOADED", NumJD = "13",
                                HeaderRep = Rep1, SupdocFl = "N", Author = "Сидоров", CBAccount = "1111122222333334444",
                                DateCreate = DateTime.Now, DateGetDoc = DateTime.Now, DateLock = DateTime.Now,
                                DateRemove = DateTime.Now  },
                new AddInfRep { Form="405", Section = "2.1", Status = "NEW", IdRep = Rep1.Id,
                                IrbPaymentRK = 5, PaymentRK = 5, CNum = "3", NumDVK = "4", Source = "LOADED", NumJD = "14",
                                HeaderRep = Rep1, SupdocFl = "N", Author = "Иванов", CBAccount = "1111122222333334445",
                                DateCreate = DateTime.Now , DateGetDoc = DateTime.Now, DateLock = DateTime.Now , DateRemove = DateTime.Now    }
            };

            context.AddInfReps.AddRange(dataAddInfReps);


            var dataBasicReps = new List<BasicRep>()
            {
                new BasicRep { AddInfRep = dataAddInfReps[0], IdOper = dataAddInfReps[0].IdOper, Filial = "0001",
                               PostDate = startDate.AddDays(1), TypeTooling = "DOL2", OperType = 11,
                               DirectionPay = "INR", Count = 10, Share = 1.1m, Ccy = "RUB", Swift = "ALFARUMM", SwiftFil = "011",
                               AmountAll = 5.0m, AmntIncome = 10.0m, RefNum = null, CodeVO = "30010", ValueDate = createDate.AddDays(3) } ,
                new BasicRep { AddInfRep = dataAddInfReps[1], IdOper = dataAddInfReps[1].IdOper, Filial = "0002",
                               PostDate = startDate.AddDays(2), TypeTooling = "DOL3", OperType = 12,
                               DirectionPay = "INR", Count = 10, Share = 1.1m, Ccy = "AUD", Swift = "SABRRUMM", SwiftFil = "011",
                               AmountAll = 7.0m, AmntIncome = 11.0m, RefNum = null, CodeVO = "30020", ValueDate = createDate.AddDays(4) },
                new BasicRep { AddInfRep = dataAddInfReps[2], IdOper = dataAddInfReps[2].IdOper, Filial = "0001",
                               PostDate = startDate.AddDays(3), TypeTooling = "DOL4", OperType = 13,
                               DirectionPay = "OUTR", Count = 10, Share = 1.1m, Ccy = "BOB", Swift = "SABRRUMM", SwiftFil = "011",
                               AmountAll = 8.0m, AmntIncome = 11.0m, RefNum = null, CodeVO = "30020", ValueDate = createDate.AddDays(4) },
                new BasicRep { AddInfRep = dataAddInfReps[3], IdOper = dataAddInfReps[3].IdOper, Filial = "0002",
                               PostDate = startDate.AddDays(4), TypeTooling = "BON2", OperType = 14,
                               DirectionPay = "OUTR", Count = 10, Share = 1.1m, Ccy = "BYN", Swift = "SABRRUMM", SwiftFil = "KR1",
                               AmountAll = 7.0m, AmntIncome = 11.0m, RefNum = null, CodeVO = "30020", ValueDate = createDate.AddDays(5) },
                new BasicRep { AddInfRep = dataAddInfReps[4], IdOper = dataAddInfReps[4].IdOper, Filial = "0009",
                               PostDate = startDate.AddDays(5), TypeTooling = "BON7", OperType = 30,
                               DirectionPay = "INN", Count = 10, Share = 1.1m, Ccy = "RUB", Swift = "SABRRUMM", SwiftFil = "KR1",
                               AmountAll = 10.0m, AmntIncome = 11.0m, RefNum = null, CodeVO = "30020", ValueDate = createDate.AddDays(7) }
            };

            context.BasicReps.AddRange(dataBasicReps);

            var dataClientReps = new List<ClientRep>()
            {
                new ClientRep { AddInfRep = dataAddInfReps[0], IdOper = dataAddInfReps[0].IdOper,
                                NameRez = "Иванов", TypeRez = "ФЛ",
                                BicPartner = "040702752", CountryRez = "643", INN = "111111111111",
                                NameNerez = "Google Ltd", TypeNerez = "В", BankName = "DeutscheBank",
                                CountryBank = "276", CountryNerez = "276", CountryPartner = "276", CustNo = "33311100",
                                PartnerName = "UBS" },
                new ClientRep { AddInfRep = dataAddInfReps[1], IdOper = dataAddInfReps[1].IdOper,
                                NameRez = "АО \"Рога и копыта\"", TypeRez = "ЮЛ",
                                BicPartner = "040813770", CountryRez = "643", INN = "111111111112",
                                NameNerez = "AG Bosh", TypeNerez = "В", BankName = "DeutscheBank",
                                CountryBank = "276", CountryNerez = "276", CountryPartner = "276", CustNo = "55511100", PartnerName = "UBS"  },
                new ClientRep { AddInfRep = dataAddInfReps[2], IdOper = dataAddInfReps[2].IdOper,
                                NameRez = "Петров", TypeRez = "ФЛ",
                                BicPartner = "040813770", CountryRez = "643", INN = "111111111113",
                                NameNerez = "Simens", TypeNerez = "С", BankName = "DeutscheBank",
                                CountryBank = "276", CountryNerez = "276", CountryPartner = "276", CustNo = "45511100", PartnerName = "UBS"  },
                new ClientRep { AddInfRep = dataAddInfReps[3], IdOper = dataAddInfReps[3].IdOper,
                                NameRez = "АО \"MTC\"", TypeRez = "ЮЛ",
                                BicPartner = "040702752", CountryRez = "643", INN = "111111111114",
                                NameNerez = "FrontRange Inc.", TypeNerez = "В", BankName = "Дж.П. Морган Банк Интернешнл",
                                CountryBank = "840", CountryNerez = "276", CountryPartner = "840", CustNo = "66611100", PartnerName = "UBS"  },
                new ClientRep { AddInfRep = dataAddInfReps[4], IdOper = dataAddInfReps[4].IdOper,
                                NameRez = "АО \"Билайн\"", TypeRez = "ЮЛ",
                                BicPartner = "040702752", CountryRez = "643", INN = "222222222222",
                                NameNerez = "Google Ltd", TypeNerez = "В", BankName = "THE BANK OF NEW YORK MELLON, NY",
                                CountryBank = "840", CountryNerez = "840", CountryPartner = "840", CustNo = "77711100", PartnerName = "UBS" },
            };

            context.ClientReps.AddRange(dataClientReps);

            var dataAddReps = new List<AddRep>()
            {
                new AddRep { AddInfRep = dataAddInfReps[0], IdOper = dataAddInfReps[0].IdOper,
                             CcdUser = "CCD User 1", Narrative = "Назначение 1", CityCode = "495",
                             NoticeReport = "Примечание 1", DepAcc = "0001", Source405 = "Источник 405_1",
                             ContractNum = "1773411103517000041", ModifyDate = createDate.AddDays(3), ModifyUser = "Петров"  },
                new AddRep { AddInfRep = dataAddInfReps[1], IdOper = dataAddInfReps[1].IdOper,
                              CcdUser = "CCD User 2", Narrative = "Назначение 2", CityCode = "812",
                             NoticeReport = "Примечание 2", DepAcc = "0002", Source405 = "Источник 405_2",
                             ContractNum = "2773411103517000041", ModifyDate = createDate.AddDays(4), ModifyUser = "Иванов"  },
                new AddRep { AddInfRep = dataAddInfReps[2], IdOper = dataAddInfReps[2].IdOper,
                             CcdUser = "CCD User 3", Narrative = "Назначение 3", CityCode = "495",
                             NoticeReport = "Примечание 3", DepAcc = "0003", Source405 = "Источник 405_3",
                             ContractNum = "1773411103517000054", ModifyDate = createDate.AddDays(5), ModifyUser = "Петров"  },
                new AddRep { AddInfRep = dataAddInfReps[3], IdOper = dataAddInfReps[3].IdOper,
                              CcdUser = "CCD User 4", Narrative = "Назначение 4", CityCode = "495",
                             NoticeReport = "Примечание 4", DepAcc = "0004", Source405 = "Источник 405_4",
                             ContractNum = "1773411103517000043", ModifyDate = createDate.AddDays(6), ModifyUser = "Иванов"  },
                new AddRep { AddInfRep = dataAddInfReps[4], IdOper = dataAddInfReps[4].IdOper,
                             CcdUser = "CCD User 5", Narrative = "Назначение 5", CityCode = "812",
                             NoticeReport = "Примечание 5", DepAcc = "0005", Source405 = "Источник 405_5",
                             ContractNum = "1773411103517000041", ModifyDate = createDate.AddDays(7), ModifyUser = "Сидоров"  },
            };

            context.AddReps.AddRange(dataAddReps);


            var dataIssuerReps = new List<IssuerRep>()
            {
                new IssuerRep { AddInfRep = dataAddInfReps[0], IdOper = dataAddInfReps[0].IdOper,
                                IssuerName = "MTC", SecurityCode = "7734202860", RegNumIssuer = "2120521001122",
                                DateRegIssuer = startDate.AddYears(-1), RepayDate = startDate.AddMonths(5),
                                CcyIssuer = "RUB", IssuerCode = 2, IssuerReestr = "56823-D"  },
                new IssuerRep { AddInfRep = dataAddInfReps[1], IdOper = dataAddInfReps[1].IdOper,
                                IssuerName = "Билайн", SecurityCode = "7734202850", RegNumIssuer = "2120521001133",
                                DateRegIssuer = startDate.AddYears(-1), RepayDate = startDate.AddMonths(6),
                                CcyIssuer = "RUB", IssuerCode = 2, IssuerReestr = "32978-E"  },
                new IssuerRep { AddInfRep = dataAddInfReps[2], IdOper = dataAddInfReps[2].IdOper,
                                IssuerName = "Индустриальный банк", SecurityCode = "7734202861", RegNumIssuer = "2120521001135",
                                DateRegIssuer = startDate.AddYears(-2), RepayDate = startDate.AddMonths(3),
                                CcyIssuer = "RUB", IssuerCode = 2, IssuerReestr = "32979-E"  },
                new IssuerRep { AddInfRep = dataAddInfReps[3], IdOper = dataAddInfReps[3].IdOper,
                                IssuerName = "Сбербанк", SecurityCode = "7734202862", RegNumIssuer = "2120521001137",
                                DateRegIssuer = startDate.AddYears(-2), RepayDate = startDate.AddMonths(7),
                                CcyIssuer = "RUB", IssuerCode = 0, IssuerReestr = "46823-E"  },
                new IssuerRep { AddInfRep = dataAddInfReps[4], IdOper = dataAddInfReps[4].IdOper,
                                IssuerName = "Райффайзенбанк", SecurityCode = "7734202863", RegNumIssuer = "2120521001145",
                                DateRegIssuer = startDate.AddYears(-3), RepayDate = startDate.AddMonths(8),
                                CcyIssuer = "AUD", IssuerCode = 2, IssuerReestr = "16823-E"  }
            };

            context.IssuerReps.AddRange(dataIssuerReps);

            var dataNoticeReps = new List<NoticeRep>()
            {
                new NoticeRep { AddInfRep = dataAddInfReps[0], IdOper = dataAddInfReps[0].IdOper, Notice = "Примечание 10",
                                NoticeIssuer = "Прим. 11", NoticeExchange = "Прим. 12", NoticeInst = "Прим. 13",
                                NoticeProperty = "Прим. 14", NoticeBank = "Прим. 15"},
                new NoticeRep { AddInfRep = dataAddInfReps[1], IdOper = dataAddInfReps[1].IdOper, Notice = "Примечание 20",
                                NoticeIssuer = "Прим. 21", NoticeExchange = "Прим. 22", NoticeInst = "Прим. 23",
                                NoticeProperty = "Прим. 24", NoticeBank = "Прим. 25"},
                new NoticeRep { AddInfRep = dataAddInfReps[2], IdOper = dataAddInfReps[2].IdOper, Notice = "Примечание 30",
                                NoticeIssuer = "Прим. 31", NoticeExchange = "Прим. 32", NoticeInst = "Прим. 33",
                                NoticeProperty = "Прим. 34", NoticeBank = "Прим. 35"},
                new NoticeRep { AddInfRep = dataAddInfReps[3], IdOper = dataAddInfReps[3].IdOper, Notice = "Примечание 40",
                                NoticeIssuer = "Прим. 41", NoticeExchange = "Прим. 42", NoticeInst = "Прим. 43",
                                NoticeProperty = "Прим. 44", NoticeBank = "Прим. 45"},
                new NoticeRep { AddInfRep = dataAddInfReps[4], IdOper = dataAddInfReps[4].IdOper, Notice = "Примечание 50",
                                NoticeIssuer = "Прим. 51", NoticeExchange = "Прим. 52", NoticeInst = "Прим. 53",
                                NoticeProperty = "Прим. 54", NoticeBank = "Прим. 55"}
            };

            context.NoticeReps.AddRange(dataNoticeReps);

            var dataRep406 = new List<Rep406>()
            {
                new Rep406 { AddInfRep = dataAddInfReps[1], IdOper = dataAddInfReps[1].IdOper, OperKind = 20,
                             Notice40 = "Прим. 11", AmountTo = 2.2m, AmountFrom = 4.4m, BicBank = "044525225",
                             CountryBank406 = "643", AddCode10 = "2", KindRez = "Покупатель", KindNerez = "С",
                             KindContract = "OC", TypeContract = "П", Notice15 = "Прим. 12",
                             Notice16  = "Прим. 13", NoticeOther = "Прим. 14" },
                new Rep406 { AddInfRep = dataAddInfReps[3], IdOper = dataAddInfReps[3].IdOper, OperKind = 40,
                             Notice40 = "Прим. 22", AmountTo = 2.2m, AmountFrom = 4.4m, BicBank = "044525225",
                             CountryBank406 = "643", AddCode10 = "3", KindRez = "Продавец", KindNerez = "С",
                             KindContract = "OC", TypeContract = "Р", Notice15 = "Прим. 22",
                             Notice16  = "Прим. 23", NoticeOther = "Прим. 24" }
            };

            context.Rep406s.AddRange(dataRep406);

            var dataSupdocReps = new List<SupdocRep>()
            {
                new SupdocRep { AddInfRep = dataAddInfReps[0], IdOper = dataAddInfReps[0].IdOper,
                       CodeSupdoc = "01_3", SupdocName = "Doc 1", Prinvest = 1, EntryDate = startDate.AddDays(3),
                       SupdocType = "Р", RegNumBranch = "0001", ClientName = "Рога и копыта",
                       SourceSupdoc = "Наименование док. 1", PassportNum = "101010101", DocDate = startDate.AddDays(4),
                       UpdateSdDate = startDate.AddDays(5)},
                new SupdocRep { AddInfRep = dataAddInfReps[1], IdOper = dataAddInfReps[1].IdOper,
                       CodeSupdoc = "01_4", SupdocName = "Doc 2", Prinvest = 2, EntryDate = startDate.AddDays(4),
                       SupdocType = "Р", RegNumBranch = "0002", ClientName = "MTC",
                       SourceSupdoc = "Наименование док. 2", PassportNum = "201010101", DocDate = startDate.AddDays(5),
                       UpdateSdDate = startDate.AddDays(6)},
                new SupdocRep { AddInfRep = dataAddInfReps[2], IdOper = dataAddInfReps[2].IdOper,
                        CodeSupdoc = "03_3", SupdocName = "Doc 3", Prinvest = 3, EntryDate = startDate.AddDays(7),
                       SupdocType = "Р", RegNumBranch = "0001", ClientName = "Билайн",
                       SourceSupdoc = "Наименование док. 3", PassportNum = "301010101", DocDate = startDate.AddDays(8),
                       UpdateSdDate = startDate.AddDays(9)},
                new SupdocRep { AddInfRep = dataAddInfReps[3], IdOper = dataAddInfReps[3].IdOper,
                       CodeSupdoc = "03_3", SupdocName = "Doc 4", Prinvest = 4, EntryDate = startDate.AddDays(1),
                       SupdocType = "Р", RegNumBranch = "0002", ClientName = "Мегафон",
                       SourceSupdoc = "Наименование док. 4", PassportNum = "201010104", DocDate = startDate.AddDays(2),
                       UpdateSdDate = startDate.AddDays(3)},
                new SupdocRep { AddInfRep = dataAddInfReps[4], IdOper = dataAddInfReps[4].IdOper,
                       CodeSupdoc = "07_4", SupdocName = "Doc 5", Prinvest = 1, EntryDate = startDate.AddDays(3),
                       SupdocType = "Р", RegNumBranch = "0001", ClientName = "Теле 2",
                       SourceSupdoc = "Наименование док. 5", PassportNum = "201010111", DocDate = startDate.AddDays(4),
                       UpdateSdDate = startDate.AddDays(5)},
            };

            context.SupdocReps.AddRange(dataSupdocReps);


            /*
            var dataCommentsReps = new List<CommentsRep>()
            {
                new CommentsRep { AddInfRep = dataAddInfReps[0], IdOper = dataAddInfReps[0].IdOper,
                    UserId = null, Comments = "bla bla bla 1" },
                new CommentsRep { AddInfRep = dataAddInfReps[1], IdOper = dataAddInfReps[1].IdOper,
                    UserId = null, Comments = "bla bla bla 2" },
                new CommentsRep { AddInfRep = dataAddInfReps[2], IdOper = dataAddInfReps[2].IdOper,
                    UserId = null, Comments = "bla bla bla 3" },
                new CommentsRep { AddInfRep = dataAddInfReps[3], IdOper = dataAddInfReps[3].IdOper,
                    UserId = null, Comments = "bla bla bla 4" },
                new CommentsRep { AddInfRep = dataAddInfReps[4], IdOper = dataAddInfReps[4].IdOper,
                    UserId = null, Comments = "bla bla bla 5" }
            };

            context.CommentsReps.AddRange(dataCommentsReps);
            */
            
            TestingApiFilters test_filters = new TestingApiFilters(context);
            //   test_filters.Tests(dataHeaderReps[0], 6, dataAddInfReps.Count);
            test_filters.AllTests(dataHeaderReps[0], dataAddInfReps.Count);
        }

    }
}