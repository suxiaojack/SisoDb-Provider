﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SisoDb.Tests.IntegrationTests.Sql2008.UnitOfWork.Queries
{
    [TestFixture]
    public class Sql2008UnitOfWorkGetByIdsTests : Sql2008IntegrationTestBase
    {
        protected override void OnTestFinalize()
        {
            DropStructureSet<IdentityItemForGetQueries>();
            DropStructureSet<GuidItemForGetQueries>();
        }

        [Test]
        public void GetByIds_WhenSeveralIdentityIds_MatchingSubsetIsReturned()
        {
            var items = new List<IdentityItemForGetQueries>
                        {
                            new IdentityItemForGetQueries {SortOrder = 1, StringValue = "A"},
                            new IdentityItemForGetQueries {SortOrder = 2, StringValue = "B"},
                            new IdentityItemForGetQueries {SortOrder = 3, StringValue = "C"}
                        };
            List<IdentityItemForGetQueries> refetched;

            using (var uow = Database.CreateUnitOfWork())
            {
                uow.InsertMany(items);
                uow.Commit();

                refetched = uow.GetByIds<IdentityItemForGetQueries>(1, 3).ToList();
            }

            Assert.AreEqual(2, refetched.Count);
            CustomAssert.AreValueEqual(items[0], refetched[0]);
            CustomAssert.AreValueEqual(items[2], refetched[1]);
        }

        [Test]
        public void GetByIds_WhenSpecifyingNonExistingIdentity_MatchingSubsetIsReturned()
        {
            var nonExistingIdentity = 99;
            var items = new List<IdentityItemForGetQueries>
                        {
                            new IdentityItemForGetQueries {SortOrder = 1, StringValue = "A"},
                            new IdentityItemForGetQueries {SortOrder = 2, StringValue = "B"},
                            new IdentityItemForGetQueries {SortOrder = 3, StringValue = "C"}
                        };
            List<IdentityItemForGetQueries> refetched;

            using (var uow = Database.CreateUnitOfWork())
            {
                uow.InsertMany(items);
                uow.Commit();

                refetched = uow.GetByIds<IdentityItemForGetQueries>(1, 3, nonExistingIdentity).ToList();
            }

            Assert.AreEqual(2, refetched.Count);
            CustomAssert.AreValueEqual(items[0], refetched[0]);
            CustomAssert.AreValueEqual(items[2], refetched[1]);
        }

        [Test]
        public void GetByIdsAsJson_WhenSeveralIdentityIds_MatchingSubsetIsReturned()
        {
            var items = new List<IdentityItemForGetQueries>
                        {
                            new IdentityItemForGetQueries {SortOrder = 1, StringValue = "A"},
                            new IdentityItemForGetQueries {SortOrder = 2, StringValue = "B"},
                            new IdentityItemForGetQueries {SortOrder = 3, StringValue = "C"}
                        };
            List<string> refetched;

            using (var uow = Database.CreateUnitOfWork())
            {
                uow.InsertMany(items);
                uow.Commit();

                refetched = uow.GetByIdsAsJson<IdentityItemForGetQueries>(1, 3).ToList();
            }

            Assert.AreEqual(2, refetched.Count);
            Assert.AreEqual("{\"StructureId\":1,\"SortOrder\":1,\"StringValue\":\"A\"}", refetched[0]);
            Assert.AreEqual("{\"StructureId\":3,\"SortOrder\":3,\"StringValue\":\"C\"}", refetched[1]);
        }

        [Test]
        public void GetByIdsAs_WhenSeveralIdentityIds_MatchingSubsetIsReturned()
        {
            var items = new List<IdentityItemForGetQueries>
                        {
                            new IdentityItemForGetQueries {SortOrder = 1, StringValue = "A"},
                            new IdentityItemForGetQueries {SortOrder = 2, StringValue = "B"},
                            new IdentityItemForGetQueries {SortOrder = 3, StringValue = "C"}
                        };
            List<View> refetched;

            using (var uow = Database.CreateUnitOfWork())
            {
                uow.InsertMany(items);
                uow.Commit();

                refetched = uow.GetByIdsAs<IdentityItemForGetQueries, View>(1, 3).ToList();
            }

            Assert.AreEqual(2, refetched.Count);
            Assert.AreEqual(1, refetched[0].SortOrder);
            Assert.AreEqual("A", refetched[0].StringValue);
            Assert.AreEqual(3, refetched[1].SortOrder);
            Assert.AreEqual("C", refetched[1].StringValue);
        }

        [Test]
        public void GetByIds_WhenSeveralGuidIds_MatchingSubsetIsReturned()
        {
            var items = new List<GuidItemForGetQueries>
                        {
                            new GuidItemForGetQueries {SortOrder = 1, StringValue = "A"},
                            new GuidItemForGetQueries {SortOrder = 2, StringValue = "B"},
                            new GuidItemForGetQueries {SortOrder = 3, StringValue = "C"}
                        };
            List<GuidItemForGetQueries> refetched;

            using (var uow = Database.CreateUnitOfWork())
            {
                uow.InsertMany(items);
                uow.Commit();

                refetched = uow.GetByIds<GuidItemForGetQueries>(items[0].StructureId, items[2].StructureId).ToList();
            }

            Assert.AreEqual(2, refetched.Count);
            CustomAssert.AreValueEqual(items[0], refetched[0]);
            CustomAssert.AreValueEqual(items[2], refetched[1]);
        }

        [Test]
        public void GetByIds_WhenSpecifyingNonExistingGuid_MatchingSubsetIsReturned()
        {
            var nonExistingGuid = Guid.Empty;
            var items = new List<GuidItemForGetQueries>
                        {
                            new GuidItemForGetQueries {SortOrder = 1, StringValue = "A"},
                            new GuidItemForGetQueries {SortOrder = 2, StringValue = "B"},
                            new GuidItemForGetQueries {SortOrder = 3, StringValue = "C"}
                        };
            List<GuidItemForGetQueries> refetched;

            using (var uow = Database.CreateUnitOfWork())
            {
                uow.InsertMany(items);
                uow.Commit();

                refetched = uow.GetByIds<GuidItemForGetQueries>(items[0].StructureId, items[2].StructureId, nonExistingGuid).ToList();
            }

            Assert.AreEqual(2, refetched.Count);
            CustomAssert.AreValueEqual(items[0], refetched[0]);
            CustomAssert.AreValueEqual(items[2], refetched[1]);
        }

        [Test]
        public void GetByIdsAsJson_WhenSeveralGuidIds_MatchingSubsetIsReturned()
        {
            var items = new List<GuidItemForGetQueries>
                        {
                            new GuidItemForGetQueries {SortOrder = 1, StringValue = "A"},
                            new GuidItemForGetQueries {SortOrder = 2, StringValue = "B"},
                            new GuidItemForGetQueries {SortOrder = 3, StringValue = "C"}
                        };
            List<string> refetched;

            using (var uow = Database.CreateUnitOfWork())
            {
                uow.InsertMany(items);
                uow.Commit();

                refetched = uow.GetByIdsAsJson<GuidItemForGetQueries>(items[0].StructureId, items[2].StructureId).ToList();
            }

            Assert.AreEqual(2, refetched.Count);
            Assert.AreEqual("{\"StructureId\":\"" + items[0].StructureId.ToString("N") + "\",\"SortOrder\":1,\"StringValue\":\"A\"}", refetched[0]);
            Assert.AreEqual("{\"StructureId\":\"" + items[2].StructureId.ToString("N") + "\",\"SortOrder\":3,\"StringValue\":\"C\"}", refetched[1]);
        }

        [Test]
        public void GetByIdsAs_WhenSeveralGuidIds_MatchingSubsetIsReturned()
        {
            var ids = new[]
                      {
                          Guid.Parse("B5CB06F0-F853-4BF2-9BED-2B1E4D703A7A"),
                          Guid.Parse("158FD134-1A3A-462B-A11B-7853D9D5B668"),
                          Guid.Parse("5C3FFE86-21D3-4C60-9519-F6C198992F07")
                      };
            var items = new List<GuidItemForGetQueries>
                        {
                            new GuidItemForGetQueries {StructureId = ids[0], SortOrder = 1, StringValue = "A"},
                            new GuidItemForGetQueries {StructureId = ids[1], SortOrder = 2, StringValue = "B"},
                            new GuidItemForGetQueries {StructureId = ids[2], SortOrder = 3, StringValue = "C"}
                        };
            List<View> refetched;

            using (var uow = Database.CreateUnitOfWork())
            {
                uow.InsertMany(items);
                uow.Commit();

                refetched = uow.GetByIdsAs<GuidItemForGetQueries, View>(items[0].StructureId, items[2].StructureId).ToList();
            }

            Assert.AreEqual(2, refetched.Count);
            Assert.AreEqual(1, refetched[0].SortOrder);
            Assert.AreEqual("A", refetched[0].StringValue);
            Assert.AreEqual(3, refetched[1].SortOrder);
            Assert.AreEqual("C", refetched[1].StringValue);
        }

        private class IdentityItemForGetQueries
        {
            public int StructureId { get; set; }

            public int SortOrder { get; set; }

            public string StringValue { get; set; }
        }

        private class GuidItemForGetQueries
        {
            public Guid StructureId { get; set; }

            public int SortOrder { get; set; }

            public string StringValue { get; set; }
        }

        private class View
        {
            public int SortOrder { get; set; }

            public string StringValue { get; set; }
        }
    }
}