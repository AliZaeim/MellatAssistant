using DataLayer.Entities.Blogs;
using DataLayer.Entities.InsPolicy;
using DataLayer.Entities.InsPolicy.CarBody;
using DataLayer.Entities.InsPolicy.Fire;
using DataLayer.Entities.InsPolicy.Liability;
using DataLayer.Entities.InsPolicy.Life;
using DataLayer.Entities.InsPolicy.ThirdParty;
using DataLayer.Entities.InsPolicy.Travel;
using DataLayer.Entities.Permissions;
using DataLayer.Entities.Supplementary;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;

namespace DataLayer.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }
        #region User
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        #endregion
        #region Supplementary
        public DbSet<WorkWith> WorkWiths { get; set; }
        public DbSet<CoWorker> CoWorkers { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Instagram> Instagrams { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<UploadCenter> UploadCenters { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<EmailBank> EmailBanks { get; set; }
        public DbSet<InsStatus> InsStatuses { get; set; }
        public DbSet<FinancialStatus> FinancialStatuses { get; set; }
        public DbSet<AdminSlider> AdminSliders { get; set; }
        public DbSet<AdminSpecialOffer> AdminSpecialOffers { get; set; }
        public DbSet<AdminHelpInfo> AdminHelpInfos { get; set; }
        public DbSet<WebsiteUpdate> WebsiteUpdates { get; set; }
        public DbSet<Cooperation> Cooperations { get; set; }
        #endregion
        #region Permissions
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        #endregion Permissons        
        #region Blogs
        public DbSet<BlogGroup> BlogGroups { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        #endregion Blogs
        #region ThirdPary
        public DbSet<ThirdParty> ThirdParties { get; set; }
        public DbSet<BasicThirdPartyPremium> BasicThirdPartyPremiums { get; set; }
        public DbSet<FinancialDamage> FinancialDamages { get; set; }
        public DbSet<FinancialPremium> FinancialPremiums { get; set; }
        public DbSet<IncidentCover> IncidentCovers { get; set; }
        public DbSet<LegalDiscount> LegalDiscounts { get; set; }
        public DbSet<LoosLifeDamage> LoosLifeDamages { get; set; }
        public DbSet<LoosDriverDamage> LoosDriverDamages { get; set; }
        public DbSet<ThirdPartyBaseData> ThirdPartyBaseDatas { get; set; }
        public DbSet<VehicleConstructionYearLimit> VehicleConstructionYearLimits { get; set; }
        public DbSet<VehicleGroup> VehicleGroups { get; set; }
        public DbSet<VehicleGroupUsage> VehicleGroupUsages { get; set; }
        public DbSet<VehicleUsage> VehicleUsages { get; set; }
        public DbSet<ThirdPartyStatus> ThirdPartyStatuses { get; set; }
        public DbSet<ThirdPartyFainancialStatus> ThirdPartyFainancialStatuses { get; set; }
        public DbSet<ThirdPartyStatusComment> ThirdPartyStatusComments { get; set; }
        public DbSet<ThirdPartySupplement> ThirdPartySupplements { get; set; }
        public DbSet<InsurerType> InsurerTypes { get; set; }

        #endregion ThirdPary
        #region LifeInsurance
        public DbSet<LifeInsurance> LifeInsurances { get; set; }
        public DbSet<PlanPaymentMethod> PlanPaymentMethods { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<LifeInsuranceStatus> LifeInsuranceStatuses { get; set; }
        public DbSet<LifeInsuranceFinancialStatus> LifeInsuranceFinancialStatuses { get; set; }
        public DbSet<LifeInsuranceSupplement> LifeInsuranceSupplements { get; set; }
        public DbSet<LifeInsuranceStatusComment> LifeInsuranceStatusComments { get; set; }

        #endregion
        #region FireInsurance 
        public DbSet<BuildingUsage> BuildingUsages { get; set; }
        public DbSet<BuildingUsageFireCoverage> BuildingUsageFireCoverages { get; set; }
        public DbSet<FireInsCoverage> FireInsCoverages { get; set; }
        public DbSet<FireInsStateRate> FireInsStateRates { get; set; }
        public DbSet<FireInsurance> FireInsurances { get; set; }
        public DbSet<FireInsuranceFinancialStatus> FireInsuranceFinancialStatuses { get; set; }
        public DbSet<FireInsuranceStatus> FireInsuranceStatuses { get; set; }
        public DbSet<FireInsuranceStatusComment> FireInsuranceStatusComments { get; set; }
        public DbSet<FireInsuranceSupplement> FireInsuranceSupplements { get; set; }
        public DbSet<FireInsurerType> FireInsurerTypes { get; set; }
        public DbSet<FireLegalDiscount> FireLegalDiscounts { get; set; }
        public DbSet<FireStructureType> FireStructureTypes { get; set; }
        public DbSet<FireBaseInfo> FireBaseInfos { get; set; }
        #endregion
        #region CarBody
        public DbSet<CarBodyInsurance> CarBodyInsurances { get; set; }
        public DbSet<CarBodyInsuranceFinancialStatus> CarBodyInsuranceFinancialStatuses { get; set; }
        public DbSet<CarBodyInsuranceStatus> CarBodyInsuranceStatuses { get; set; }
        public DbSet<CarBodyStatusComment> CarBodyStatusComments { get; set; }
        public DbSet<CarBodySupplement> CarBodySupplements { get; set; }
        public DbSet<CarBodyCar> CarBodyCars { get; set; }
        public DbSet<CarBodyCarGroup> CarBodyCarGroups { get; set; }
        public DbSet<CarBodyGroupUsage> CarBodyGroupUsages { get; set; }
        public DbSet<CarBodyUsage> CarBodyUsages { get; set; }
        public DbSet<CarBodyCover> CarBodyCovers { get; set; }
        public DbSet<CarBodyInsurerType> CarBodyInsurerTypes { get; set; }
        public DbSet<CarBodyInsuranceType> CarBodyInsuranceTypes { get; set; }
        public DbSet<CarBodyLegalDiscount> CarBodyLegalDiscounts { get; set; }
        #endregion
        #region Liability
        public DbSet<LiabilityInsurance> LiabilityInsurances { get; set; }
        public DbSet<LiabilityStatus> LiabilityStatuses { get; set; }
        public DbSet<LiabilityFinancialStatus> LiabilityFinancialStatuses { get; set; }
        public DbSet<LiabilityStatusComment> LiabilityStatusComments { get; set; }
        public DbSet<LiabilitySupplement> LiabilitySupplements { get; set; }
        #endregion
        #region Travel
        public DbSet<TravelInsCo> TravelInsCos { get; set; }
        public DbSet<TravelFinancialStatus> TravelFinancialStatuses { get; set; }
        public DbSet<TravelStatus> TravelStatuses { get; set; }
        public DbSet<TravelStatusComment> TravelStatusComments { get; set; }
        public DbSet<TravelSupplement> TravelSupplements { get; set; }
        public DbSet<TravelInsClass> TravelInsClasses { get; set; }
        public DbSet<TravelZoom> TravelZooms { get; set; }
        public DbSet<TravelClassZoom> TravelClassZooms { get; set; }
        public DbSet<TravelInsurance> TravelInsurances { get; set; }
        #endregion
        #region InsPolicy
        public DbSet<CollectionCommissionBase> CollectionCommissionBases { get; set; }
        public DbSet<CollectionCommissionDetails> CollectionCommissionDetails { get; set; }
        #endregion
        public static string EncodePasswordtoMd5(string pass) //Encrypt using MD5   
        {
            byte[] data = MD5.HashData(Encoding.UTF8.GetBytes(pass));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRole>()
              .HasQueryFilter(r => !r.IsDeleted);
            modelBuilder.Entity<InsStatus>()
               .HasQueryFilter(r => !r.IsDeleted);
            modelBuilder.Entity<BlogGroup>().HasQueryFilter(r => !r.IsDeleted);
            modelBuilder.Entity<Conversation>().HasQueryFilter(r => !r.IsDeleted);
            
    
            #region آذربایجان شرقی
            modelBuilder.Entity<State>().HasData(
                new State
                {
                    StateId = 1,
                    StateName = "آذربایجان شرقی"
                }

                );
            modelBuilder.Entity<County>().HasData(

                    new County { StateId = 1, CountyId = 1, CountyName = "کشکسرای" },
                    new County { StateId = 1, CountyId = 2, CountyName = "سهند" },
                    new County { StateId = 1, CountyId = 3, CountyName = "سیس" },
                    new County { StateId = 1, CountyId = 4, CountyName = "دوزدوزان" },
                    new County { StateId = 1, CountyId = 5, CountyName = "تیمورلو" },
                    new County { StateId = 1, CountyId = 6, CountyName = "صوفیان" },
                    new County { StateId = 1, CountyId = 7, CountyName = "سردرود" },
                    new County { StateId = 1, CountyId = 8, CountyName = "هادیشهر" },
                    new County { StateId = 1, CountyId = 9, CountyName = "هشترود" },
                    new County { StateId = 1, CountyId = 10, CountyName = "زرنق" },
                    new County { StateId = 1, CountyId = 11, CountyName = "ترکمانچای" },
                    new County { StateId = 1, CountyId = 12, CountyName = "ورزقان" },
                    new County { StateId = 1, CountyId = 13, CountyName = "تسوج" },
                    new County { StateId = 1, CountyId = 14, CountyName = "زنوز" },
                    new County { StateId = 1, CountyId = 15, CountyName = "ایخچی" },
                    new County { StateId = 1, CountyId = 16, CountyName = "شزفخانه" },
                    new County { StateId = 1, CountyId = 17, CountyName = "مهربان" },
                    new County { StateId = 1, CountyId = 18, CountyName = "مبارک شهر" },
                    new County { StateId = 1, CountyId = 19, CountyName = "تیکمه داش" },
                    new County { StateId = 1, CountyId = 20, CountyName = "باسمنج" },
                    new County { StateId = 1, CountyId = 21, CountyName = "سیه رود" },
                    new County { StateId = 1, CountyId = 22, CountyName = "میانه" },
                    new County { StateId = 1, CountyId = 23, CountyName = "خمارلو" },
                    new County { StateId = 1, CountyId = 24, CountyName = "خواجه" },
                    new County { StateId = 1, CountyId = 25, CountyName = "بناب مرند" },
                    new County { StateId = 1, CountyId = 26, CountyName = "قره آغاج" },
                    new County { StateId = 1, CountyId = 27, CountyName = "وایقان" },
                    new County { StateId = 1, CountyId = 28, CountyName = "مراغه" },
                    new County { StateId = 1, CountyId = 29, CountyName = "ممقان" },
                    new County { StateId = 1, CountyId = 30, CountyName = "خامنه" },
                    new County { StateId = 1, CountyId = 31, CountyName = "خسروشاه" },
                    new County { StateId = 1, CountyId = 32, CountyName = "لیلان" },
                    new County { StateId = 1, CountyId = 33, CountyName = "نظرکهریزی" },
                    new County { StateId = 1, CountyId = 34, CountyName = "اهر" },
                    new County { StateId = 1, CountyId = 35, CountyName = "بخشایش" },
                    new County { StateId = 1, CountyId = 36, CountyName = "آقکند" },
                    new County { StateId = 1, CountyId = 37, CountyName = "جوان قلعه" },
                    new County { StateId = 1, CountyId = 38, CountyName = "کلیبر" },
                    new County { StateId = 1, CountyId = 39, CountyName = "مرند" },
                    new County { StateId = 1, CountyId = 40, CountyName = "اسکو" },
                    new County { StateId = 1, CountyId = 41, CountyName = "شندآباد" },
                    new County { StateId = 1, CountyId = 42, CountyName = "شربیان" },
                    new County { StateId = 1, CountyId = 43, CountyName = "گوگان" },
                    new County { StateId = 1, CountyId = 44, CountyName = "بستان آباد" },
                    new County { StateId = 1, CountyId = 45, CountyName = "تبریز" },
                    new County { StateId = 1, CountyId = 46, CountyName = "جلفا" },
                    new County { StateId = 1, CountyId = 47, CountyName = "آچاچی" },
                    new County { StateId = 1, CountyId = 48, CountyName = "هریس" },
                    new County { StateId = 1, CountyId = 49, CountyName = "یامچی" },
                    new County { StateId = 1, CountyId = 50, CountyName = "خاروانا" },
                    new County { StateId = 1, CountyId = 51, CountyName = "کوزه کنان" },
                    new County { StateId = 1, CountyId = 52, CountyName = "خداجو" },
                    new County { StateId = 1, CountyId = 53, CountyName = "آذرشهر" },
                    new County { StateId = 1, CountyId = 54, CountyName = "شبستر" },
                    new County { StateId = 1, CountyId = 55, CountyName = "سراب" },
                    new County { StateId = 1, CountyId = 56, CountyName = "ملکان" },
                    new County { StateId = 1, CountyId = 57, CountyName = "بناب" },
                    new County { StateId = 1, CountyId = 58, CountyName = "هوراند" },
                    new County { StateId = 1, CountyId = 59, CountyName = "کلوانق" },
                    new County { StateId = 1, CountyId = 60, CountyName = "ترک" },
                    new County { StateId = 1, CountyId = 61, CountyName = "عجب شیر" },
                    new County { StateId = 1, CountyId = 62, CountyName = "آبش احمد" }


                );
            #endregion آذربایجان شرقی
            #region آذربایجان غربی
            modelBuilder.Entity<State>().HasData(
                    new State
                    {
                        StateId = 2,
                        StateName = "آذربایجان غربی"
                    }

                );
            modelBuilder.Entity<County>().HasData(
                    new County { CountyName = "تازه شهر", StateId = 2, CountyId = 63 },
                   new County { CountyName = "نالوس", StateId = 2, CountyId = 64 },
                   new County { CountyName = "ایواوغلی", StateId = 2, CountyId = 65 },
                   new County { CountyName = "شاهین دژ", StateId = 2, CountyId = 66 },
                   new County { CountyName = "گردکشانه", StateId = 2, CountyId = 67 },
                   new County { CountyName = "باروق", StateId = 2, CountyId = 68 },
                   new County { CountyName = "سیلوانه", StateId = 2, CountyId = 69 },
                   new County { CountyName = "بازرگان", StateId = 2, CountyId = 70 },
                   new County { CountyName = "نازک علیا", StateId = 2, CountyId = 71 },
                   new County { CountyName = "ربط", StateId = 2, CountyId = 72 },
                   new County { CountyName = "تکاب", StateId = 2, CountyId = 73 },
                   new County { CountyName = "دیزج دیز", StateId = 2, CountyId = 74 },
                   new County { CountyName = "سیمینه", StateId = 2, CountyId = 75 },
                   new County { CountyName = "نوشین", StateId = 2, CountyId = 76 },
                   new County { CountyName = "میاندوآب", StateId = 2, CountyId = 77 },
                   new County { CountyName = "مرگنلر", StateId = 2, CountyId = 78 },
                   new County { CountyName = "سلماس", StateId = 2, CountyId = 79 },
                   new County { CountyName = "آواجیق", StateId = 2, CountyId = 80 },
                   new County { CountyName = "قطور", StateId = 2, CountyId = 81 },
                   new County { CountyName = "محمودآباد", StateId = 2, CountyId = 82 },
                   new County { CountyName = "خوی", StateId = 2, CountyId = 83 },
                   new County { CountyName = "نقده", StateId = 2, CountyId = 84 },
                   new County { CountyName = "سرو", StateId = 2, CountyId = 85 },
                   new County { CountyName = "خلیفان", StateId = 2, CountyId = 86 },
                   new County { CountyName = "پلدشت", StateId = 2, CountyId = 87 },
                   new County { CountyName = "میرآباد", StateId = 2, CountyId = 88 },
                   new County { CountyName = "اشنویه", StateId = 2, CountyId = 89 },
                   new County { CountyName = "زرآباد", StateId = 2, CountyId = 90 },
                   new County { CountyName = "بوکان", StateId = 2, CountyId = 91 },
                   new County { CountyName = "پیرانشهر", StateId = 2, CountyId = 92 },
                   new County { CountyName = "چهاربرج", StateId = 2, CountyId = 93 },
                   new County { CountyName = "قوشچی", StateId = 2, CountyId = 94 },
                   new County { CountyName = "شوط", StateId = 2, CountyId = 95 },
                   new County { CountyName = "ماکو", StateId = 2, CountyId = 96 },
                   new County { CountyName = "سیه چشمه", StateId = 2, CountyId = 97 },
                   new County { CountyName = "سردشت", StateId = 2, CountyId = 98 },
                   new County { CountyName = "کشاورز", StateId = 2, CountyId = 99 },
                   new County { CountyName = "فیرورق", StateId = 2, CountyId = 100 },
                   new County { CountyName = "محمدیار", StateId = 2, CountyId = 101 },
                   new County { CountyName = "ارومیه", StateId = 2, CountyId = 102 },
                   new County { CountyName = "مهاباد", StateId = 2, CountyId = 103 },
                   new County { CountyName = "قره ضیاءالدین", StateId = 2, CountyId = 104 }
                );
            #endregion آذربایجان غربی
            #region اردبیل
            modelBuilder.Entity<State>().HasData(
                new State
                {
                    StateId = 3,
                    StateName = "اردبیل"
                }

            );
            modelBuilder.Entity<County>().HasData(

                    new County { CountyName = "پارس آباد", StateId = 3, CountyId = 105 },
                    new County { CountyName = "فخر آباد", StateId = 3, CountyId = 106 },
                    new County { CountyName = "کلور", StateId = 3, CountyId = 107 },
                    new County { CountyName = "نیر", StateId = 3, CountyId = 108 },
                    new County { CountyName = "اردبیل", StateId = 3, CountyId = 109 },
                    new County { CountyName = "اسلام آباد", StateId = 3, CountyId = 110 },
                    new County { CountyName = "تازه کندانگوت", StateId = 3, CountyId = 111 },
                    new County { CountyName = "مشگین شهر", StateId = 3, CountyId = 112 },
                    new County { CountyName = "جعفر آباد", StateId = 3, CountyId = 113 },
                    new County { CountyName = "نمین", StateId = 3, CountyId = 114 },
                    new County { CountyName = "اصلاندوز", StateId = 3, CountyId = 115 },
                    new County { CountyName = "مرادلو", StateId = 3, CountyId = 116 },
                    new County { CountyName = "خلخال", StateId = 3, CountyId = 117 },
                    new County { CountyName = "کوراییم", StateId = 3, CountyId = 118 },
                    new County { CountyName = "هیر", StateId = 3, CountyId = 119 },
                    new County { CountyName = "گیوی", StateId = 3, CountyId = 120 },
                    new County { CountyName = "گرمی", StateId = 3, CountyId = 121 },
                    new County { CountyName = "لاهرود", StateId = 3, CountyId = 122 },
                    new County { CountyName = "هشتجین", StateId = 3, CountyId = 123 },
                    new County { CountyName = "عنبران", StateId = 3, CountyId = 124 },
                    new County { CountyName = "تازه کند", StateId = 3, CountyId = 125 },
                    new County { CountyName = "قصابه", StateId = 3, CountyId = 126 },
                    new County { CountyName = "رضی", StateId = 3, CountyId = 127 },
                    new County { CountyName = "سرعین", StateId = 3, CountyId = 128 },
                    new County { CountyName = "بیله سوار", StateId = 3, CountyId = 129 },
                    new County { CountyName = "آبی بیگلو", StateId = 3, CountyId = 130 }

                );
            #endregion اردبیل
            #region اصفهان
            modelBuilder.Entity<State>().HasData(
                new State
                {
                    StateId = 4,
                    StateName = "اصفهان"
                }

            );

            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "آبی بیگلو", StateId = 4, CountyId = 131 },
                new County { CountyName = "زیار", StateId = 4, CountyId = 132 },
                new County { CountyName = "زرین شهر", StateId = 4, CountyId = 133 },
                new County { CountyName = "گلشن", StateId = 4, CountyId = 134 },
                new County { CountyName = "پیربکران", StateId = 4, CountyId = 135 },
                new County { CountyName = "خالدآباد", StateId = 4, CountyId = 136 },
                new County { CountyName = "سجزی", StateId = 4, CountyId = 137 },
                new County { CountyName = "گوگد", StateId = 4, CountyId = 138 },
                new County { CountyName = "تیران", StateId = 4, CountyId = 139 },
                new County { CountyName = "ونک", StateId = 4, CountyId = 140 },
                new County { CountyName = "دهق", StateId = 4, CountyId = 141 },
                new County { CountyName = "زواره", StateId = 4, CountyId = 142 },
                new County { CountyName = "کاشان", StateId = 4, CountyId = 143 },
                new County { CountyName = "ابوزید آباد", StateId = 4, CountyId = 144 },
                new County { CountyName = "اصغر آباد", StateId = 4, CountyId = 145 },
                new County { CountyName = "بافران", StateId = 4, CountyId = 146 },
                new County { CountyName = "شهرضا", StateId = 4, CountyId = 147 },
                new County { CountyName = "خور", StateId = 4, CountyId = 148 },
                new County { CountyName = "مجلسی", StateId = 4, CountyId = 149 },
                new County { CountyName = "هرند", StateId = 4, CountyId = 150 },
                new County { CountyName = "فولادشهر", StateId = 4, CountyId = 151 },
                new County { CountyName = "کمشچه", StateId = 4, CountyId = 152 },
                new County { CountyName = "کلیشادوسودرجان", StateId = 4, CountyId = 153 },
                new County { CountyName = "لای بید", StateId = 4, CountyId = 154 },
                new County { CountyName = "قهجاورستان", StateId = 4, CountyId = 155 },
                new County { CountyName = "چرمین", StateId = 4, CountyId = 156 },
                new County { CountyName = "رزوه", StateId = 4, CountyId = 157 },
                new County { CountyName = "فریدونشهر", StateId = 4, CountyId = 158 },
                new County { CountyName = "طرق آباد", StateId = 4, CountyId = 159 },
                new County { CountyName = "نصر آباد", StateId = 4, CountyId = 160 },
                new County { CountyName = "برزک", StateId = 4, CountyId = 161 },
                new County { CountyName = "سفید شهر", StateId = 4, CountyId = 162 },
                new County { CountyName = "سمیرم", StateId = 4, CountyId = 163 },
                new County { CountyName = "گلدشت", StateId = 4, CountyId = 164 },
                new County { CountyName = "اردستان", StateId = 4, CountyId = 165 },
                new County { CountyName = "جوشقان قالی", StateId = 4, CountyId = 166 },
                new County { CountyName = "بویین و میاندشت", StateId = 4, CountyId = 167 },
                new County { CountyName = "کرکوند", StateId = 4, CountyId = 168 },
                new County { CountyName = "درچه", StateId = 4, CountyId = 169 },
                new County { CountyName = "انارک", StateId = 4, CountyId = 170 },
                new County { CountyName = "دولت آباد", StateId = 4, CountyId = 171 },
                new County { CountyName = "ایمانشهر", StateId = 4, CountyId = 172 },
                new County { CountyName = "گرگاب", StateId = 4, CountyId = 173 },
                new County { CountyName = "حسن آباد", StateId = 4, CountyId = 174 },
                new County { CountyName = "سده لنجان", StateId = 4, CountyId = 175 },
                new County { CountyName = "حبیب آباد", StateId = 4, CountyId = 176 },
                new County { CountyName = "بهاران", StateId = 4, CountyId = 177 },
                new County { CountyName = "میمه", StateId = 4, CountyId = 178 },
                new County { CountyName = "تودشک", StateId = 4, CountyId = 179 },
                new County { CountyName = "گلشهر", StateId = 4, CountyId = 180 },
                new County { CountyName = "رضوانشهر", StateId = 4, CountyId = 181 },
                new County { CountyName = "داران", StateId = 4, CountyId = 182 },
                new County { CountyName = "علویجه", StateId = 4, CountyId = 183 },
                new County { CountyName = "نیک آباد", StateId = 4, CountyId = 184 },
                new County { CountyName = "مشکات", StateId = 4, CountyId = 185 },
                new County { CountyName = "آران و بیدگل", StateId = 4, CountyId = 186 },
                new County { CountyName = "خوانسار", StateId = 4, CountyId = 187 },
                new County { CountyName = "نجف آباد", StateId = 4, CountyId = 188 },
                new County { CountyName = "منظریه", StateId = 4, CountyId = 189 },
                new County { CountyName = "فرخی", StateId = 4, CountyId = 190 },
                new County { CountyName = "دیزیچه", StateId = 4, CountyId = 191 },
                new County { CountyName = "اژیه", StateId = 4, CountyId = 192 },
                new County { CountyName = "زاینده رود", StateId = 4, CountyId = 193 },
                new County { CountyName = "خورزوق", StateId = 4, CountyId = 194 },
                new County { CountyName = "قهدریجان", StateId = 4, CountyId = 195 },
                new County { CountyName = "شاهین شهر", StateId = 4, CountyId = 196 },
                new County { CountyName = "بهارستان", StateId = 4, CountyId = 197 },
                new County { CountyName = "چمگردان", StateId = 4, CountyId = 198 },
                new County { CountyName = "دهاقان", StateId = 4, CountyId = 199 },
                new County { CountyName = "برف انبار", StateId = 4, CountyId = 200 },
                new County { CountyName = "بادرود", StateId = 4, CountyId = 201 },
                new County { CountyName = "کوهپایه", StateId = 4, CountyId = 202 },
                new County { CountyName = "گلپایگان", StateId = 4, CountyId = 203 },
                new County { CountyName = "عسگران", StateId = 4, CountyId = 204 },
                new County { CountyName = "حنا", StateId = 4, CountyId = 205 },
                new County { CountyName = "کهریزسنگ", StateId = 4, CountyId = 206 },
                new County { CountyName = "مهاباد", StateId = 4, CountyId = 207 },
                new County { CountyName = "کامو و چوگان", StateId = 4, CountyId = 208 },
                new County { CountyName = "افوس", StateId = 4, CountyId = 209 },
                new County { CountyName = "زیباشهر", StateId = 4, CountyId = 300 },
                new County { CountyName = "کوشک", StateId = 4, CountyId = 301 },
                new County { CountyName = "نایین", StateId = 4, CountyId = 302 },
                new County { CountyName = "سین", StateId = 4, CountyId = 303 },
                new County { CountyName = "زازران", StateId = 4, CountyId = 304 },
                new County { CountyName = "مبارکه", StateId = 4, CountyId = 305 },
                new County { CountyName = "ورزنه", StateId = 4, CountyId = 306 },
                new County { CountyName = "ورنامخواست", StateId = 4, CountyId = 307 },
                new County { CountyName = "شاپور آباد", StateId = 4, CountyId = 308 },
                new County { CountyName = "فلاورجان", StateId = 4, CountyId = 309 },
                new County { CountyName = "وزوان", StateId = 4, CountyId = 310 },
                new County { CountyName = "اصفهان", StateId = 4, CountyId = 311 },
                new County { CountyName = "باغ بهادران", StateId = 4, CountyId = 312 },
                new County { CountyName = "چادگان", StateId = 4, CountyId = 313 },
                new County { CountyName = "دامنه", StateId = 4, CountyId = 314 },
                new County { CountyName = "نطنز", StateId = 4, CountyId = 315 },
                new County { CountyName = "محمد آباد", StateId = 4, CountyId = 316 },
                new County { CountyName = "نیاسر", StateId = 4, CountyId = 317 },
                new County { CountyName = "نوش آباد", StateId = 4, CountyId = 318 },
                new County { CountyName = "کمه", StateId = 4, CountyId = 319 },
                new County { CountyName = "جوزدان", StateId = 4, CountyId = 320 },
                new County { CountyName = "قمصر", StateId = 4, CountyId = 321 },
                new County { CountyName = "جندق", StateId = 4, CountyId = 322 },
                new County { CountyName = "طالخونچه", StateId = 4, CountyId = 323 },
                new County { CountyName = "خمینی شهر", StateId = 4, CountyId = 324 },
                new County { CountyName = "باغشاد", StateId = 4, CountyId = 325 },
                new County { CountyName = "دستگرد", StateId = 4, CountyId = 326 },
                new County { CountyName = "ابریشم", StateId = 4, CountyId = 327 }
                );

            #endregion اصفهان
            #region البرز
            modelBuilder.Entity<State>().HasData(
                new State
                {
                    StateId = 5,
                    StateName = "البرز"
                }

            );
            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "چهارباغ", StateId = 5, CountyId = 328 },
                new County { CountyName = "آسارا", StateId = 5, CountyId = 329 },
                new County { CountyName = "کرج", StateId = 5, CountyId = 330 },
                new County { CountyName = "طالقان", StateId = 5, CountyId = 331 },
                new County { CountyName = "شهرجدید هشتگرد", StateId = 5, CountyId = 332 },
                new County { CountyName = "محمدشهر", StateId = 5, CountyId = 333 },
                new County { CountyName = "مشکین دشت", StateId = 5, CountyId = 334 },
                new County { CountyName = "نظرآباد", StateId = 5, CountyId = 335 },
                new County { CountyName = "هشتگرد", StateId = 5, CountyId = 336 },
                new County { CountyName = "ماهدشت", StateId = 5, CountyId = 337 },
                new County { CountyName = "اشتهارد", StateId = 5, CountyId = 338 },
                new County { CountyName = "کوهسار", StateId = 5, CountyId = 339 },
                new County { CountyName = "گرمدره", StateId = 5, CountyId = 340 },
                new County { CountyName = "تنکمان", StateId = 5, CountyId = 341 },
                new County { CountyName = "گلسار", StateId = 5, CountyId = 342 },
                new County { CountyName = "کمال شهر", StateId = 5, CountyId = 343 },
                new County { CountyName = "فردیس", StateId = 5, CountyId = 344 }

                );
            #endregion البرز
            #region ایلام
            modelBuilder.Entity<State>().HasData(
               new State
               {
                   StateId = 6,
                   StateName = "ایلام"
               }

           );
            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "آبدانان", StateId = 6, CountyId = 345 },
                new County { CountyName = "شباب", StateId = 6, CountyId = 346 },
                new County { CountyName = "موسیان", StateId = 6, CountyId = 347 },
                new County { CountyName = "بدره", StateId = 6, CountyId = 348 },
                new County { CountyName = "ایلام", StateId = 6, CountyId = 349 },
                new County { CountyName = "ایوان", StateId = 6, CountyId = 350 },
                new County { CountyName = "مهران", StateId = 6, CountyId = 351 },
                new County { CountyName = "آسمان آباد", StateId = 6, CountyId = 352 },
                new County { CountyName = "پهله", StateId = 6, CountyId = 353 },
                new County { CountyName = "مهر", StateId = 6, CountyId = 354 },
                new County { CountyName = "سراب باغ", StateId = 6, CountyId = 355 },
                new County { CountyName = "بلاوه", StateId = 6, CountyId = 356 },
                new County { CountyName = "میمه", StateId = 6, CountyId = 357 },
                new County { CountyName = "دره شهر", StateId = 6, CountyId = 358 },
                new County { CountyName = "ارکواز", StateId = 6, CountyId = 359 },
                new County { CountyName = "مورموری", StateId = 6, CountyId = 360 },
                new County { CountyName = "توحید", StateId = 6, CountyId = 361 },
                new County { CountyName = "دهلران", StateId = 6, CountyId = 362 },
                new County { CountyName = "لومار", StateId = 6, CountyId = 363 },
                new County { CountyName = "چوار", StateId = 6, CountyId = 364 },
                new County { CountyName = "زرنه", StateId = 6, CountyId = 365 },
                new County { CountyName = "صالح آباد", StateId = 6, CountyId = 366 },
                new County { CountyName = "سرابله", StateId = 6, CountyId = 367 },
                new County { CountyName = "ماژین", StateId = 6, CountyId = 368 },
                new County { CountyName = "دلگشا", StateId = 6, CountyId = 369 }

            );
            #endregion ایلام
            #region بوشهر
            modelBuilder.Entity<State>().HasData(
                new State
                {
                    StateId = 7,
                    StateName = "بوشهر"
                }

            );
            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "ریز", StateId = 7, CountyId = 370 },
                new County { CountyName = "برازجان", StateId = 7, CountyId = 371 },
                new County { CountyName = "بندر ریگ", StateId = 7, CountyId = 372 },
                new County { CountyName = "اهرم", StateId = 7, CountyId = 373 },
                new County { CountyName = "دوراهک", StateId = 7, CountyId = 374 },
                new County { CountyName = "خورموج", StateId = 7, CountyId = 375 },
                new County { CountyName = "نخل تقی", StateId = 7, CountyId = 376 },
                new County { CountyName = "کلمه", StateId = 7, CountyId = 377 },
                new County { CountyName = "بندر دیلم", StateId = 7, CountyId = 378 },
                new County { CountyName = "وحدتیه", StateId = 7, CountyId = 379 },
                new County { CountyName = "بنک", StateId = 7, CountyId = 380 },
                new County { CountyName = "چغادک", StateId = 7, CountyId = 381 },
                new County { CountyName = "بندر دیر", StateId = 7, CountyId = 382 },
                new County { CountyName = "کاکی", StateId = 7, CountyId = 383 },
                new County { CountyName = "جم", StateId = 7, CountyId = 384 },
                new County { CountyName = "دالکی", StateId = 7, CountyId = 385 },
                new County { CountyName = "بندر گناوه", StateId = 7, CountyId = 386 },
                new County { CountyName = "آباد", StateId = 7, CountyId = 387 },
                new County { CountyName = "آبدان", StateId = 7, CountyId = 388 },
                new County { CountyName = "خارک", StateId = 7, CountyId = 389 },
                new County { CountyName = "شنبه", StateId = 7, CountyId = 390 },
                new County { CountyName = "بوشکان", StateId = 7, CountyId = 391 },
                new County { CountyName = "انارستان", StateId = 7, CountyId = 392 },
                new County { CountyName = "شبانکاره", StateId = 7, CountyId = 393 },
                new County { CountyName = "سیراف", StateId = 7, CountyId = 394 },
                new County { CountyName = "دلوار", StateId = 7, CountyId = 395 },
                new County { CountyName = "بردستان", StateId = 7, CountyId = 396 },
                new County { CountyName = "بادوله", StateId = 7, CountyId = 397 },
                new County { CountyName = "عسلویه", StateId = 7, CountyId = 398 },
                new County { CountyName = "تنگ ارم", StateId = 7, CountyId = 399 },
                new County { CountyName = "امام حسن", StateId = 7, CountyId = 400 },
                new County { CountyName = "سعد آباد", StateId = 7, CountyId = 401 },
                new County { CountyName = "بندر کنگان", StateId = 7, CountyId = 402 },
                new County { CountyName = "بوشهر", StateId = 7, CountyId = 403 },
                new County { CountyName = "بردخون", StateId = 7, CountyId = 404 },
                new County { CountyName = "آب پخش", StateId = 7, CountyId = 405 }

                );
            #endregion بوشهر
            #region تهران
            modelBuilder.Entity<State>().HasData(
                new State
                {
                    StateId = 8,
                    StateName = "تهران"
                }

            );
            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "شاهدشهر", StateId = 8, CountyId = 406 },
                new County { CountyName = "پیشوا", StateId = 8, CountyId = 407 },
                new County { CountyName = "جوادآباد", StateId = 8, CountyId = 408 },
                new County { CountyName = "ارجمند", StateId = 8, CountyId = 409 },
                new County { CountyName = "ری", StateId = 8, CountyId = 410 },
                new County { CountyName = "نصیر شهر", StateId = 8, CountyId = 411 },
                new County { CountyName = "رودهن", StateId = 8, CountyId = 412 },
                new County { CountyName = "اندیشه", StateId = 8, CountyId = 413 },
                new County { CountyName = "نسیم شهر", StateId = 8, CountyId = 414 },
                new County { CountyName = "صبا شهر", StateId = 8, CountyId = 415 },
                new County { CountyName = "ملارد", StateId = 8, CountyId = 416 },
                new County { CountyName = "شمشک", StateId = 8, CountyId = 417 },
                new County { CountyName = "پاکدشت", StateId = 8, CountyId = 418 },
                new County { CountyName = "باقرشهر", StateId = 8, CountyId = 419 },
                new County { CountyName = "احمدآباد مستوفی", StateId = 8, CountyId = 420 },
                new County { CountyName = "کیلان", StateId = 8, CountyId = 421 },
                new County { CountyName = "قرچک", StateId = 8, CountyId = 422 },
                new County { CountyName = "فردوسیه", StateId = 8, CountyId = 423 },
                new County { CountyName = "گلستان", StateId = 8, CountyId = 424 },
                new County { CountyName = "ورامین", StateId = 8, CountyId = 425 },
                new County { CountyName = "فیروزکوه", StateId = 8, CountyId = 426 },
                new County { CountyName = "فشم", StateId = 8, CountyId = 427 },
                new County { CountyName = "پرند", StateId = 8, CountyId = 428 },
                new County { CountyName = "آبعلی", StateId = 8, CountyId = 429 },
                new County { CountyName = "چهاردانگه", StateId = 8, CountyId = 430 },
                new County { CountyName = "تهران", StateId = 8, CountyId = 431 },
                new County { CountyName = "بومهن", StateId = 8, CountyId = 432 },
                new County { CountyName = "وحیدیه", StateId = 8, CountyId = 433 },
                new County { CountyName = "صفادشت", StateId = 8, CountyId = 434 },
                new County { CountyName = "لواسان", StateId = 8, CountyId = 435 },
                new County { CountyName = "فرون آباد", StateId = 8, CountyId = 436 },
                new County { CountyName = "کهریزک", StateId = 8, CountyId = 437 },
                new County { CountyName = "رباط کریم", StateId = 8, CountyId = 438 },
                new County { CountyName = "آبسرد", StateId = 8, CountyId = 439 },
                new County { CountyName = "باغستان", StateId = 8, CountyId = 440 },
                new County { CountyName = "صالحیه", StateId = 8, CountyId = 441 },
                new County { CountyName = "شهریار", StateId = 8, CountyId = 442 },
                new County { CountyName = "قدس", StateId = 8, CountyId = 443 },
                new County { CountyName = "تجریش", StateId = 8, CountyId = 444 },
                new County { CountyName = "شریف آباد", StateId = 8, CountyId = 445 },
                new County { CountyName = "حسن آباد", StateId = 8, CountyId = 446 },
                new County { CountyName = "اسلامشهر", StateId = 8, CountyId = 447 },
                new County { CountyName = "دماوند", StateId = 8, CountyId = 448 },
                new County { CountyName = "پردیس", StateId = 8, CountyId = 449 }

            );

            #endregion تهران
            #region چهار محال و بختیاری 
            modelBuilder.Entity<State>().HasData(
                new State
                {
                    StateId = 9,
                    StateName = "چهار محال و بختیاری"
                }

            );
            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "وردنجان", StateId = 9, CountyId = 450 },
                new County { CountyName = "گوجان", StateId = 9, CountyId = 451 },
                new County { CountyName = "گهرو", StateId = 9, CountyId = 452 },
                new County { CountyName = "سورشجان", StateId = 9, CountyId = 453 },
                new County { CountyName = "سرخون", StateId = 9, CountyId = 454 },
                new County { CountyName = "شهرکرد", StateId = 9, CountyId = 455 },
                new County { CountyName = "منج", StateId = 9, CountyId = 456 },
                new County { CountyName = "بروجن", StateId = 9, CountyId = 457 },
                new County { CountyName = "پردنجان", StateId = 9, CountyId = 458 },
                new County { CountyName = "سامان", StateId = 9, CountyId = 459 },
                new County { CountyName = "فرخ شهر", StateId = 9, CountyId = 460 },
                new County { CountyName = "صمصامی", StateId = 9, CountyId = 461 },
                new County { CountyName = "طاقانک", StateId = 9, CountyId = 462 },
                new County { CountyName = "کاج", StateId = 9, CountyId = 463 },
                new County { CountyName = "نقنه", StateId = 9, CountyId = 464 },
                new County { CountyName = "لردگان", StateId = 9, CountyId = 465 },
                new County { CountyName = "باباحیدر", StateId = 9, CountyId = 466 },
                new County { CountyName = "دستنا", StateId = 9, CountyId = 467 },
                new County { CountyName = "سودجان", StateId = 9, CountyId = 468 },
                new County { CountyName = "بازفت", StateId = 9, CountyId = 469 },
                new County { CountyName = "هفشجان", StateId = 9, CountyId = 470 },
                new County { CountyName = "سردشت", StateId = 9, CountyId = 471 },
                new County { CountyName = "فرادنبه", StateId = 9, CountyId = 472 },
                new County { CountyName = "چلیچه", StateId = 9, CountyId = 473 },
                new County { CountyName = "بن", StateId = 9, CountyId = 474 },
                new County { CountyName = "فارسان", StateId = 9, CountyId = 475 },
                new County { CountyName = "شلمزار", StateId = 9, CountyId = 476 },
                new County { CountyName = "نافچ", StateId = 9, CountyId = 477 },
                new County { CountyName = "دشتک", StateId = 9, CountyId = 478 },
                new County { CountyName = "بلداجی", StateId = 9, CountyId = 479 },
                new County { CountyName = "آلونی", StateId = 9, CountyId = 480 },
                new County { CountyName = "گندمان", StateId = 9, CountyId = 481 },
                new County { CountyName = "جونقان", StateId = 9, CountyId = 482 },
                new County { CountyName = "ناغان", StateId = 9, CountyId = 483 },
                new County { CountyName = "هارونی", StateId = 9, CountyId = 484 },
                new County { CountyName = "چلگرد", StateId = 9, CountyId = 485 },
                new County { CountyName = "کیان", StateId = 9, CountyId = 486 },
                new County { CountyName = "اردل", StateId = 9, CountyId = 487 },
                new County { CountyName = "سفیددشت", StateId = 9, CountyId = 488 },
                new County { CountyName = "مال خلیفه", StateId = 9, CountyId = 489 }

                );
            #endregion چهار مهال و بختیاری
            #region خراسان جنوبی
            modelBuilder.Entity<State>().HasData(
               new State
               {
                   StateId = 10,
                   StateName = "خراسان جنوبی"
               });

            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "اسلامیه", StateId = 10, CountyId = 490 },
                new County { CountyName = "شوسف", StateId = 10, CountyId = 491 },
                new County { CountyName = "قاین", StateId = 10, CountyId = 492 },
                new County { CountyName = "عشق آباد", StateId = 10, CountyId = 493 },
                new County { CountyName = "طبس مسینا", StateId = 10, CountyId = 494 },
                new County { CountyName = "ارسک", StateId = 10, CountyId = 495 },
                new County { CountyName = "آیسک", StateId = 10, CountyId = 496 },
                new County { CountyName = "نیمبلوک", StateId = 10, CountyId = 497 },
                new County { CountyName = "دیهوک", StateId = 10, CountyId = 498 },
                new County { CountyName = "سر بیشه", StateId = 10, CountyId = 499 },
                new County { CountyName = "محمدشهر", StateId = 10, CountyId = 500 },
                new County { CountyName = "بیرجند", StateId = 10, CountyId = 501 },
                new County { CountyName = "فردوس", StateId = 10, CountyId = 502 },
                new County { CountyName = "نهبندان", StateId = 10, CountyId = 503 },
                new County { CountyName = "اسفدن", StateId = 10, CountyId = 504 },
                new County { CountyName = "گزیک", StateId = 10, CountyId = 505 },
                new County { CountyName = "حاجی آباد", StateId = 10, CountyId = 506 },
                new County { CountyName = "سه قلعه", StateId = 10, CountyId = 507 },
                new County { CountyName = "آرین شهر", StateId = 10, CountyId = 508 },
                new County { CountyName = "مود", StateId = 10, CountyId = 509 },
                new County { CountyName = "خوسف", StateId = 10, CountyId = 510 },
                new County { CountyName = "قهستان", StateId = 10, CountyId = 511 },
                new County { CountyName = "بشرویه", StateId = 10, CountyId = 512 },
                new County { CountyName = "سرایان", StateId = 10, CountyId = 513 },
                new County { CountyName = "خضری دشت بیاض", StateId = 10, CountyId = 514 },
                new County { CountyName = "طبس", StateId = 10, CountyId = 515 },
                new County { CountyName = "اسدیه", StateId = 10, CountyId = 516 },
                new County { CountyName = "زهان", StateId = 10, CountyId = 517 }

              );
            #endregion خراسان جنوبی
            #region خراسان رضوی
            modelBuilder.Entity<State>().HasData(
                 new State
                 {
                     StateId = 11,
                     StateName = "خراسان رضوی"
                 });
            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "بار", StateId = 11, CountyId = 518 },
                new County { CountyName = "نیل شهر", StateId = 11, CountyId = 519 },
                new County { CountyName = "جنگل", StateId = 11, CountyId = 520 },
                new County { CountyName = "درود", StateId = 11, CountyId = 521 },
                new County { CountyName = "رباط سنگ", StateId = 11, CountyId = 522 },
                new County { CountyName = "سلطان آباد", StateId = 11, CountyId = 523 },
                new County { CountyName = "فریمان", StateId = 11, CountyId = 524 },
                new County { CountyName = "گناباد", StateId = 11, CountyId = 525 },
                new County { CountyName = "کاریز", StateId = 11, CountyId = 526 },
                new County { CountyName = "همت آباد", StateId = 11, CountyId = 527 },
                new County { CountyName = "سلامی", StateId = 11, CountyId = 528 },
                new County { CountyName = "باجگیران", StateId = 11, CountyId = 529 },
                new County { CountyName = "بجستان", StateId = 11, CountyId = 530 },
                new County { CountyName = "چناران", StateId = 11, CountyId = 531 },
                new County { CountyName = "درگز", StateId = 11, CountyId = 532 },
                new County { CountyName = "کلات", StateId = 11, CountyId = 533 },
                new County { CountyName = "چکنه", StateId = 11, CountyId = 534 },
                new County { CountyName = "نصرآباد", StateId = 11, CountyId = 535 },
                new County { CountyName = "بردسکن", StateId = 11, CountyId = 536 },
                new County { CountyName = "مشهد", StateId = 11, CountyId = 537 },
                new County { CountyName = "کدکن", StateId = 11, CountyId = 538 },
                new County { CountyName = "نقاب", StateId = 11, CountyId = 539 },
                new County { CountyName = "قلندرآباد", StateId = 11, CountyId = 540 },
                new County { CountyName = "کاشمر", StateId = 11, CountyId = 541 },
                new County { CountyName = "شاندیز", StateId = 11, CountyId = 542 },
                new County { CountyName = "نشتیفان", StateId = 11, CountyId = 543 },
                new County { CountyName = "ششتمد", StateId = 11, CountyId = 544 },
                new County { CountyName = "شادمهر", StateId = 11, CountyId = 545 },
                new County { CountyName = "عشق آباد", StateId = 11, CountyId = 546 },
                new County { CountyName = "چاپشلو", StateId = 11, CountyId = 547 },
                new County { CountyName = "رشتخوار", StateId = 11, CountyId = 548 },
                new County { CountyName = "قدمگاه", StateId = 11, CountyId = 549 },
                new County { CountyName = "صالح آباد", StateId = 11, CountyId = 550 },
                new County { CountyName = "داورزن", StateId = 11, CountyId = 551 },
                new County { CountyName = "فرهادگاه", StateId = 11, CountyId = 552 },
                new County { CountyName = "کاخک", StateId = 11, CountyId = 553 },
                new County { CountyName = "مشهدریزه", StateId = 11, CountyId = 554 },
                new County { CountyName = "جغتای", StateId = 11, CountyId = 555 },
                new County { CountyName = "مزدآوند", StateId = 11, CountyId = 556 },
                new County { CountyName = "قوچان", StateId = 11, CountyId = 557 },
                new County { CountyName = "یونسی", StateId = 11, CountyId = 558 },
                new County { CountyName = "سنگان", StateId = 11, CountyId = 559 },
                new County { CountyName = "نوخندان", StateId = 11, CountyId = 560 },
                new County { CountyName = "کندر", StateId = 11, CountyId = 561 },
                new County { CountyName = "نیشابور", StateId = 11, CountyId = 562 },
                new County { CountyName = "احمدآباد صولت", StateId = 11, CountyId = 563 },
                new County { CountyName = "شهرآباد", StateId = 11, CountyId = 564 },
                new County { CountyName = "رضویه", StateId = 11, CountyId = 565 },
                new County { CountyName = "تربت حیدریه", StateId = 11, CountyId = 566 },
                new County { CountyName = "باخرز", StateId = 11, CountyId = 567 },
                new County { CountyName = "سفید سنگ", StateId = 11, CountyId = 568 },
                new County { CountyName = "بیدخت", StateId = 11, CountyId = 569 },
                new County { CountyName = "تایباد", StateId = 11, CountyId = 570 },
                new County { CountyName = "فیروزه", StateId = 11, CountyId = 571 },
                new County { CountyName = "قاسم آباد", StateId = 11, CountyId = 572 },
                new County { CountyName = "سبزوار", StateId = 11, CountyId = 573 },
                new County { CountyName = "فیض آباد", StateId = 11, CountyId = 574 },
                new County { CountyName = "گلمکان", StateId = 11, CountyId = 575 },
                new County { CountyName = "لطف آباد", StateId = 11, CountyId = 576 },
                new County { CountyName = "شهرزو", StateId = 11, CountyId = 577 },
                new County { CountyName = "خرو", StateId = 11, CountyId = 578 },
                new County { CountyName = "تربت جام", StateId = 11, CountyId = 579 },
                new County { CountyName = "انابد", StateId = 11, CountyId = 580 },
                new County { CountyName = "ملک آباد", StateId = 11, CountyId = 581 },
                new County { CountyName = "بایک", StateId = 11, CountyId = 582 },
                new County { CountyName = "دولت آباد", StateId = 11, CountyId = 583 },
                new County { CountyName = "سرخس", StateId = 11, CountyId = 584 },
                new County { CountyName = "ریوش", StateId = 11, CountyId = 585 },
                new County { CountyName = "طرقبه", StateId = 11, CountyId = 586 },
                new County { CountyName = "خواف", StateId = 11, CountyId = 587 },
                new County { CountyName = "روداب", StateId = 11, CountyId = 588 },
                new County { CountyName = "خلیل آباد", StateId = 11, CountyId = 589 }

                );
            #endregion خراسان رضوی
            #region خراسان شمالی
            modelBuilder.Entity<State>().HasData(
                 new State
                 {
                     StateId = 12,
                     StateName = "خراسان شمالی"
                 });
            modelBuilder.Entity<County>().HasData(
                    new County { CountyName = "چناران شهر", StateId = 12, CountyId = 590 },
                    new County { CountyName = "راز", StateId = 12, CountyId = 591 },
                    new County { CountyName = "پیش قلعه", StateId = 12, CountyId = 592 },
                    new County { CountyName = "قوشخانه", StateId = 12, CountyId = 593 },
                    new County { CountyName = "شوقان", StateId = 12, CountyId = 594 },
                    new County { CountyName = "اسفراین", StateId = 12, CountyId = 595 },
                    new County { CountyName = "گرمه", StateId = 12, CountyId = 596 },
                    new County { CountyName = "قاضی", StateId = 12, CountyId = 597 },
                    new County { CountyName = "شیروان", StateId = 12, CountyId = 598 },
                    new County { CountyName = "خصار گرمخان", StateId = 12, CountyId = 599 },
                    new County { CountyName = "آشخانه", StateId = 12, CountyId = 600 },
                    new County { CountyName = "تیتکانلو", StateId = 12, CountyId = 601 },
                    new County { CountyName = "جاجرم", StateId = 12, CountyId = 602 },
                    new County { CountyName = "بجنورد", StateId = 12, CountyId = 603 },
                    new County { CountyName = "درق", StateId = 12, CountyId = 604 },
                    new County { CountyName = "آوا", StateId = 12, CountyId = 605 },
                    new County { CountyName = "زیارت", StateId = 12, CountyId = 606 },
                    new County { CountyName = "سنخواست", StateId = 12, CountyId = 607 },
                    new County { CountyName = "صفی آباد", StateId = 12, CountyId = 608 },
                    new County { CountyName = "ایور", StateId = 12, CountyId = 609 },
                    new County { CountyName = "فاروج", StateId = 12, CountyId = 610 },
                    new County { CountyName = "لوجلی", StateId = 12, CountyId = 611 }

                );
            #endregion خراسان شمالی
            #region خوزستان
            modelBuilder.Entity<State>().HasData(
                new State
                {
                    StateId = 13,
                    StateName = "خوزستان"
                });
            modelBuilder.Entity<County>().HasData(
                    new County { CountyName = "هفتگل", StateId = 13, CountyId = 612 },
                    new County { CountyName = "بیدروبه", StateId = 13, CountyId = 613 },
                    new County { CountyName = "شاوور", StateId = 13, CountyId = 614 },
                    new County { CountyName = "حمزه", StateId = 13, CountyId = 615 },
                    new County { CountyName = "گتوند", StateId = 13, CountyId = 616 },
                    new County { CountyName = "شرافت", StateId = 13, CountyId = 617 },
                    new County { CountyName = "منصوریه", StateId = 13, CountyId = 618 },
                    new County { CountyName = "زهره", StateId = 13, CountyId = 619 },
                    new County { CountyName = "رامهرمز", StateId = 13, CountyId = 620 },
                    new County { CountyName = "بندر امام خمینی", StateId = 13, CountyId = 621 },
                    new County { CountyName = "کوت عبداله", StateId = 13, CountyId = 622 },
                    new County { CountyName = "میداود", StateId = 13, CountyId = 623 },
                    new County { CountyName = "چغامیش", StateId = 13, CountyId = 624 },
                    new County { CountyName = "ملاثانی", StateId = 13, CountyId = 625 },
                    new County { CountyName = "چم گلک", StateId = 13, CountyId = 626 },
                    new County { CountyName = "حر", StateId = 13, CountyId = 627 },
                    new County { CountyName = "شمس آباد", StateId = 13, CountyId = 628 },
                    new County { CountyName = "آبژدان", StateId = 13, CountyId = 629 },
                    new County { CountyName = "چوبیده", StateId = 13, CountyId = 630 },
                    new County { CountyName = "مسجد سلیمان", StateId = 13, CountyId = 631 },
                    new County { CountyName = "مقاومت", StateId = 13, CountyId = 632 },
                    new County { CountyName = "ترکالکی", StateId = 13, CountyId = 633 },
                    new County { CountyName = "دارخوین", StateId = 13, CountyId = 634 },
                    new County { CountyName = "سردشت", StateId = 13, CountyId = 635 },
                    new County { CountyName = "لالی", StateId = 13, CountyId = 636 },
                    new County { CountyName = "کوت سید نعیم", StateId = 13, CountyId = 637 },
                    new County { CountyName = "حمیدیه", StateId = 13, CountyId = 638 },
                    new County { CountyName = "دهدز", StateId = 13, CountyId = 639 },
                    new County { CountyName = "قلعه تل", StateId = 13, CountyId = 640 },
                    new County { CountyName = "میانرود", StateId = 13, CountyId = 641 },
                    new County { CountyName = "رفیع", StateId = 13, CountyId = 642 },
                    new County { CountyName = "اندیمشک", StateId = 13, CountyId = 643 },
                    new County { CountyName = "الوان", StateId = 13, CountyId = 644 },
                    new County { CountyName = "سالند", StateId = 13, CountyId = 645 },
                    new County { CountyName = "صالح شهر", StateId = 13, CountyId = 646 },
                    new County { CountyName = "اروندکنار", StateId = 13, CountyId = 647 },
                    new County { CountyName = "سرداران", StateId = 13, CountyId = 648 },
                    new County { CountyName = "تشان", StateId = 13, CountyId = 649 },
                    new County { CountyName = "رامشیر", StateId = 13, CountyId = 650 },
                    new County { CountyName = "شادگان", StateId = 13, CountyId = 651 },
                    new County { CountyName = "بندر ماهشهر", StateId = 13, CountyId = 652 },
                    new County { CountyName = "جایزان", StateId = 13, CountyId = 653 },
                    new County { CountyName = "بستان", StateId = 13, CountyId = 654 },
                    new County { CountyName = "ویس", StateId = 13, CountyId = 655 },
                    new County { CountyName = "اهواز", StateId = 13, CountyId = 656 },
                    new County { CountyName = "فتح المبین", StateId = 13, CountyId = 657 },
                    new County { CountyName = "شهر امام", StateId = 13, CountyId = 658 },
                    new County { CountyName = "قلعه خواجه", StateId = 13, CountyId = 659 },
                    new County { CountyName = "حسینیه", StateId = 13, CountyId = 660 },
                    new County { CountyName = "گلگیر", StateId = 13, CountyId = 661 },
                    new County { CountyName = "مینوشهر", StateId = 13, CountyId = 662 },
                    new County { CountyName = "سماله", StateId = 13, CountyId = 663 },
                    new County { CountyName = "شوشتر", StateId = 13, CountyId = 664 },
                    new County { CountyName = "بهبهان", StateId = 13, CountyId = 665 },
                    new County { CountyName = "هندیجان", StateId = 13, CountyId = 666 },
                    new County { CountyName = "ابوحمیظه", StateId = 13, CountyId = 667 },
                    new County { CountyName = "آغاجاری", StateId = 13, CountyId = 668 },
                    new County { CountyName = "ایذه", StateId = 13, CountyId = 669 },
                    new County { CountyName = "صیدون", StateId = 13, CountyId = 670 },
                    new County { CountyName = "سیاه منصور", StateId = 13, CountyId = 671 },
                    new County { CountyName = "هویزه", StateId = 13, CountyId = 672 },
                    new County { CountyName = "آزادی", StateId = 13, CountyId = 673 },
                    new County { CountyName = "شوش", StateId = 13, CountyId = 674 },
                    new County { CountyName = "دزفول", StateId = 13, CountyId = 675 },
                    new County { CountyName = "جنت مکان", StateId = 13, CountyId = 676 },
                    new County { CountyName = "آبادان", StateId = 13, CountyId = 677 },
                    new County { CountyName = "گوریه", StateId = 13, CountyId = 678 },
                    new County { CountyName = "خرمشهر", StateId = 13, CountyId = 679 },
                    new County { CountyName = "مشراگه", StateId = 13, CountyId = 680 },
                    new County { CountyName = "خنافره", StateId = 13, CountyId = 681 },
                    new County { CountyName = "چمران", StateId = 13, CountyId = 682 },
                    new County { CountyName = "امیدیه", StateId = 13, CountyId = 683 },
                    new County { CountyName = "سوسنگرد", StateId = 13, CountyId = 684 },
                    new County { CountyName = "شیبان", StateId = 13, CountyId = 685 },
                    new County { CountyName = "الهایی", StateId = 13, CountyId = 686 },
                    new County { CountyName = "باغ ملک", StateId = 13, CountyId = 687 },
                    new County { CountyName = "صفی آباد", StateId = 13, CountyId = 688 }
                );
            #endregion خوزستان
            #region زنجان
            modelBuilder.Entity<State>().HasData(
                      new State
                      {
                          StateId = 14,
                          StateName = "زنجان"
                      });
            modelBuilder.Entity<County>().HasData(
                    new County { CountyName = "سجاس", StateId = 14, CountyId = 689 },
                    new County { CountyName = "زرین رود", StateId = 14, CountyId = 690 },
                    new County { CountyName = "آب بر", StateId = 14, CountyId = 691 },
                    new County { CountyName = "ارمغانخانه", StateId = 14, CountyId = 692 },
                    new County { CountyName = "کرسف", StateId = 14, CountyId = 693 },
                    new County { CountyName = "هیدج", StateId = 14, CountyId = 694 },
                    new County { CountyName = "سلطانیه", StateId = 14, CountyId = 695 },
                    new County { CountyName = "خرمدره", StateId = 14, CountyId = 696 },
                    new County { CountyName = "نیک پی", StateId = 14, CountyId = 697 },
                    new County { CountyName = "قیدار", StateId = 14, CountyId = 698 },
                    new County { CountyName = "ابهر", StateId = 14, CountyId = 699 },
                    new County { CountyName = "دندی", StateId = 14, CountyId = 700 },
                    new County { CountyName = "حلب", StateId = 14, CountyId = 701 },
                    new County { CountyName = "نور بهار", StateId = 14, CountyId = 702 },
                    new County { CountyName = "گرماب", StateId = 14, CountyId = 703 },
                    new County { CountyName = "چورزق", StateId = 14, CountyId = 704 },
                    new County { CountyName = "زنجان", StateId = 14, CountyId = 705 },
                    new County { CountyName = "سهرود", StateId = 14, CountyId = 706 },
                    new County { CountyName = "صایین قلعه", StateId = 14, CountyId = 707 },
                    new County { CountyName = "ماه نشان", StateId = 14, CountyId = 708 },
                    new County { CountyName = "زرین آباد", StateId = 14, CountyId = 709 }

                );

            #endregion زنجان
            #region سمنان
            modelBuilder.Entity<State>().HasData(
                     new State
                     {
                         StateId = 15,
                         StateName = "سمنان"
                     });
            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "ایوانکی", StateId = 15, CountyId = 710 },
                new County { CountyName = "مجن", StateId = 15, CountyId = 711 },
                new County { CountyName = "دامغان", StateId = 15, CountyId = 712 },
                new County { CountyName = "سرخه", StateId = 15, CountyId = 713 },
                new County { CountyName = "مهدی شهر", StateId = 15, CountyId = 714 },
                new County { CountyName = "شاهرود", StateId = 15, CountyId = 715 },
                new County { CountyName = "سمنان", StateId = 15, CountyId = 716 },
                new County { CountyName = "کهن آباد", StateId = 15, CountyId = 717 },
                new County { CountyName = "گرمسار", StateId = 15, CountyId = 718 },
                new County { CountyName = "کلاته خیج", StateId = 15, CountyId = 719 },
                new County { CountyName = "دیباج", StateId = 15, CountyId = 720 },
                new County { CountyName = "درجزین", StateId = 15, CountyId = 721 },
                new County { CountyName = "رودیان", StateId = 15, CountyId = 722 },
                new County { CountyName = "بسطام", StateId = 15, CountyId = 723 },
                new County { CountyName = "امیریه", StateId = 15, CountyId = 724 },
                new County { CountyName = "میامی", StateId = 15, CountyId = 725 },
                new County { CountyName = "شهمیرزاد", StateId = 15, CountyId = 726 },
                new County { CountyName = "بیارجمند", StateId = 15, CountyId = 727 },
                new County { CountyName = "کلاته", StateId = 15, CountyId = 728 },
                new County { CountyName = "آرادان", StateId = 15, CountyId = 729 }

                );
            #endregion سمنان
            #region سیستان و بلوچستان
            modelBuilder.Entity<State>().HasData(
                new State
                {
                    StateId = 16,
                    StateName = "سیستان و بلوچستان"
                });
            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "محمدی", StateId = 16, CountyId = 730 },
                new County { CountyName = "شهرک علی اکبر", StateId = 16, CountyId = 731 },
                new County { CountyName = "بنجار", StateId = 16, CountyId = 732 },
                new County { CountyName = "گلمورتی", StateId = 16, CountyId = 733 },
                new County { CountyName = "نگور", StateId = 16, CountyId = 734 },
                new County { CountyName = "راسک", StateId = 16, CountyId = 735 },
                new County { CountyName = "بنت", StateId = 16, CountyId = 736 },
                new County { CountyName = "قصرقند", StateId = 16, CountyId = 737 },
                new County { CountyName = "جالق", StateId = 16, CountyId = 738 },
                new County { CountyName = "هیدوچ", StateId = 16, CountyId = 739 },
                new County { CountyName = "نوک آباد", StateId = 16, CountyId = 740 },
                new County { CountyName = "زهک", StateId = 16, CountyId = 741 },
                new County { CountyName = "بمبپور", StateId = 16, CountyId = 742 },
                new County { CountyName = "پیشین", StateId = 16, CountyId = 743 },
                new County { CountyName = "گشت", StateId = 16, CountyId = 744 },
                new County { CountyName = "محمدآباد", StateId = 16, CountyId = 745 },
                new County { CountyName = "زاهدان", StateId = 16, CountyId = 746 },
                new County { CountyName = "زابلی", StateId = 16, CountyId = 747 },
                new County { CountyName = "چاه بهار", StateId = 16, CountyId = 748 },
                new County { CountyName = "زرآباد", StateId = 16, CountyId = 749 },
                new County { CountyName = "بزمان", StateId = 16, CountyId = 750 },
                new County { CountyName = "اسپکه", StateId = 16, CountyId = 751 },
                new County { CountyName = "فنوج", StateId = 16, CountyId = 752 },
                new County { CountyName = "سراوان", StateId = 16, CountyId = 753 },
                new County { CountyName = "ادیمی", StateId = 16, CountyId = 754 },
                new County { CountyName = "زابل", StateId = 16, CountyId = 755 },
                new County { CountyName = "دوست محمد", StateId = 16, CountyId = 756 },
                new County { CountyName = "ایرانشهر", StateId = 16, CountyId = 757 },
                new County { CountyName = "سرباز", StateId = 16, CountyId = 758 },
                new County { CountyName = "سیرکان", StateId = 16, CountyId = 759 },
                new County { CountyName = "میرجاوه", StateId = 16, CountyId = 760 },
                new County { CountyName = "نصرت آباد", StateId = 16, CountyId = 761 },
                new County { CountyName = "سوران", StateId = 16, CountyId = 762 },
                new County { CountyName = "خاش", StateId = 16, CountyId = 763 },
                new County { CountyName = "کنارک", StateId = 16, CountyId = 764 },
                new County { CountyName = "محمدان", StateId = 16, CountyId = 765 },
                new County { CountyName = "نیک شهر", StateId = 16, CountyId = 766 }

                );

            #endregion
            #region فارس
            modelBuilder.Entity<State>().HasData(
                new State
                {
                    StateId = 17,
                    StateName = "فارس"
                });
            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "کازرون", StateId = 17, CountyId = 767 },
                new County { CountyName = "کارزین", StateId = 17, CountyId = 768 },
                new County { CountyName = "فدامی", StateId = 17, CountyId = 769 },
                new County { CountyName = "خومه زار", StateId = 17, CountyId = 770 },
                new County { CountyName = "سلطان آباد", StateId = 17, CountyId = 771 },
                new County { CountyName = "فیروزآباد", StateId = 17, CountyId = 772 },
                new County { CountyName = "دبیران", StateId = 17, CountyId = 773 },
                new County { CountyName = "باب انار", StateId = 17, CountyId = 774 },
                new County { CountyName = "رامجرد", StateId = 17, CountyId = 775 },
                new County { CountyName = "سروستان", StateId = 17, CountyId = 776 },
                new County { CountyName = "قره بلاغ", StateId = 17, CountyId = 777 },
                new County { CountyName = "ارسنجان", StateId = 17, CountyId = 778 },
                new County { CountyName = "دژکرد", StateId = 17, CountyId = 779 },
                new County { CountyName = "بیرم", StateId = 17, CountyId = 780 },
                new County { CountyName = "دهرم", StateId = 17, CountyId = 781 },
                new County { CountyName = "شیراز", StateId = 17, CountyId = 782 },
                new County { CountyName = "ایزدخواست", StateId = 17, CountyId = 783 },
                new County { CountyName = "علامرودشت", StateId = 17, CountyId = 784 },
                new County { CountyName = "اوز", StateId = 17, CountyId = 785 },
                new County { CountyName = "وراوی", StateId = 17, CountyId = 786 },
                new County { CountyName = "بیضا", StateId = 17, CountyId = 787 },
                new County { CountyName = "نی ریز", StateId = 17, CountyId = 788 },
                new County { CountyName = "کنار تخته", StateId = 17, CountyId = 789 },
                new County { CountyName = "امام شهر", StateId = 17, CountyId = 790 },
                new County { CountyName = "جهرم", StateId = 17, CountyId = 791 },
                new County { CountyName = "بابامنیر", StateId = 17, CountyId = 792 },
                new County { CountyName = "گراش", StateId = 17, CountyId = 793 },
                new County { CountyName = "فسا", StateId = 17, CountyId = 794 },
                new County { CountyName = "شهرپیر", StateId = 17, CountyId = 795 },
                new County { CountyName = "حسن آباد", StateId = 17, CountyId = 796 },
                new County { CountyName = "کامفیروز", StateId = 17, CountyId = 797 },
                new County { CountyName = "خنچ", StateId = 17, CountyId = 798 },
                new County { CountyName = "خانه زنیان", StateId = 17, CountyId = 799 },
                new County { CountyName = "استهبان", StateId = 17, CountyId = 800 },
                new County { CountyName = "بوانات", StateId = 17, CountyId = 801 },
                new County { CountyName = "لطیفی", StateId = 17, CountyId = 802 },
                new County { CountyName = "فراشبند", StateId = 17, CountyId = 803 },
                new County { CountyName = "زرقان", StateId = 17, CountyId = 804 },
                new County { CountyName = "صغاد", StateId = 17, CountyId = 805 },
                new County { CountyName = "اشکنان", StateId = 17, CountyId = 806 },
                new County { CountyName = "قائمیه", StateId = 17, CountyId = 807 },
                new County { CountyName = "گله دار", StateId = 17, CountyId = 808 },
                new County { CountyName = "دوبرجی", StateId = 17, CountyId = 809 },
                new County { CountyName = "آباده طشک", StateId = 17, CountyId = 810 },
                new County { CountyName = "خرامه", StateId = 17, CountyId = 811 },
                new County { CountyName = "میمند", StateId = 17, CountyId = 812 },
                new County { CountyName = "افزر", StateId = 17, CountyId = 813 },
                new County { CountyName = "دوزه", StateId = 17, CountyId = 814 },
                new County { CountyName = "سیدان", StateId = 17, CountyId = 815 },
                new County { CountyName = "کوپن", StateId = 17, CountyId = 816 },
                new County { CountyName = "زاهدشهر", StateId = 17, CountyId = 817 },
                new County { CountyName = "قادرآباد", StateId = 17, CountyId = 818 },
                new County { CountyName = "سده", StateId = 17, CountyId = 819 },
                new County { CountyName = "بنارویه", StateId = 17, CountyId = 820 },
                new County { CountyName = "سعادت شهر", StateId = 17, CountyId = 821 },
                new County { CountyName = "شهر صدرا", StateId = 17, CountyId = 822 },
                new County { CountyName = "سورمق", StateId = 17, CountyId = 823 },
                new County { CountyName = "حسامی", StateId = 17, CountyId = 824 },
                new County { CountyName = "جویم", StateId = 17, CountyId = 825 },
                new County { CountyName = "خوزی", StateId = 17, CountyId = 826 },
                new County { CountyName = "اردکان", StateId = 17, CountyId = 827 },
                new County { CountyName = "فطرویه", StateId = 17, CountyId = 828 },
                new County { CountyName = "نودان", StateId = 17, CountyId = 829 },
                new County { CountyName = "مبارک آباددیز", StateId = 17, CountyId = 830 },
                new County { CountyName = "داراب", StateId = 17, CountyId = 831 },
                new County { CountyName = "نورآباد", StateId = 17, CountyId = 832 },
                new County { CountyName = "کوار", StateId = 17, CountyId = 833 },
                new County { CountyName = "نوبندگان", StateId = 17, CountyId = 834 },
                new County { CountyName = "حاجی آباد", StateId = 17, CountyId = 835 },
                new County { CountyName = "خاوران", StateId = 17, CountyId = 836 },
                new County { CountyName = "مرودشت", StateId = 17, CountyId = 837 },
                new County { CountyName = "کوهنجان", StateId = 17, CountyId = 838 },
                new County { CountyName = "ششده", StateId = 17, CountyId = 839 },
                new County { CountyName = "مزایجان", StateId = 17, CountyId = 840 },
                new County { CountyName = "ایج", StateId = 17, CountyId = 841 },
                new County { CountyName = "خور", StateId = 17, CountyId = 842 },
                new County { CountyName = "نوجین", StateId = 17, CountyId = 843 },
                new County { CountyName = "لپویی", StateId = 17, CountyId = 844 },
                new County { CountyName = "بهمن", StateId = 17, CountyId = 845 },
                new County { CountyName = "اهل", StateId = 17, CountyId = 846 },
                new County { CountyName = "خشت", StateId = 17, CountyId = 847 },
                new County { CountyName = "مهر", StateId = 17, CountyId = 848 },
                new County { CountyName = "جنت شهر", StateId = 17, CountyId = 849 },
                new County { CountyName = "مشکان", StateId = 17, CountyId = 850 },
                new County { CountyName = "بالاده", StateId = 17, CountyId = 851 },
                new County { CountyName = "قیر", StateId = 17, CountyId = 852 },
                new County { CountyName = "قطب آباد", StateId = 17, CountyId = 853 },
                new County { CountyName = "خانمین", StateId = 17, CountyId = 854 },
                new County { CountyName = "مصیری", StateId = 17, CountyId = 855 },
                new County { CountyName = "میانشهر", StateId = 17, CountyId = 856 },
                new County { CountyName = "صفاشهر", StateId = 17, CountyId = 857 },
                new County { CountyName = "اقلید", StateId = 17, CountyId = 858 },
                new County { CountyName = "عمادده", StateId = 17, CountyId = 859 },
                new County { CountyName = "مادر سلیمان", StateId = 17, CountyId = 860 },
                new County { CountyName = "داریان", StateId = 17, CountyId = 861 },
                new County { CountyName = "رونیز", StateId = 17, CountyId = 862 },
                new County { CountyName = "کره ای", StateId = 17, CountyId = 863 },
                new County { CountyName = "لار", StateId = 17, CountyId = 864 },
                new County { CountyName = "اسیر", StateId = 17, CountyId = 865 },
                new County { CountyName = "هماشهر", StateId = 17, CountyId = 866 },
                new County { CountyName = "آباده", StateId = 17, CountyId = 867 },
                new County { CountyName = "لامرد", StateId = 17, CountyId = 868 }
                );
            #endregion
            #region قزوین
            modelBuilder.Entity<State>().HasData(
                new State
                {
                    StateId = 18,
                    StateName = "قزوین"
                });
            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "سگزآباد", StateId = 18, CountyId = 869 },
                new County { CountyName = "بیدستان", StateId = 18, CountyId = 870 },
                new County { CountyName = "کوهین", StateId = 18, CountyId = 871 },
                new County { CountyName = "رازمیان", StateId = 18, CountyId = 872 },
                new County { CountyName = "خرمدشت", StateId = 18, CountyId = 873 },
                new County { CountyName = "آبگرم", StateId = 18, CountyId = 874 },
                new County { CountyName = "شال", StateId = 18, CountyId = 875 },
                new County { CountyName = "شریفیه", StateId = 18, CountyId = 876 },
                new County { CountyName = "اقبالیه", StateId = 18, CountyId = 877 },
                new County { CountyName = "نرجه", StateId = 18, CountyId = 878 },
                new County { CountyName = "ارداق", StateId = 18, CountyId = 879 },
                new County { CountyName = "الوند", StateId = 18, CountyId = 880 },
                new County { CountyName = "خاکعلی", StateId = 18, CountyId = 881 },
                new County { CountyName = "سیردان", StateId = 18, CountyId = 882 },
                new County { CountyName = "ضیاد آباد", StateId = 18, CountyId = 883 },
                new County { CountyName = "بوئین زهرا", StateId = 18, CountyId = 884 },
                new County { CountyName = "محمدیه", StateId = 18, CountyId = 885 },
                new County { CountyName = "محمود آباد نمونه", StateId = 18, CountyId = 886 },
                new County { CountyName = "معلم کلایه", StateId = 18, CountyId = 887 },
                new County { CountyName = "اسفرورین", StateId = 18, CountyId = 888 },
                new County { CountyName = "آوج", StateId = 18, CountyId = 889 },
                new County { CountyName = "دانسفهان", StateId = 18, CountyId = 890 },
                new County { CountyName = "آبیک", StateId = 18, CountyId = 891 },
                new County { CountyName = "قزوین", StateId = 18, CountyId = 892 },
                new County { CountyName = "تاکستان", StateId = 18, CountyId = 893 }
                );
            #endregion
            #region قم
            modelBuilder.Entity<State>().HasData(
                new State
                {
                    StateId = 19,
                    StateName = "قم"
                });
            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "کهک", StateId = 19, CountyId = 894 },
                new County { CountyName = "قم", StateId = 19, CountyId = 895 },
                new County { CountyName = "سلفچگان", StateId = 19, CountyId = 896 },
                new County { CountyName = "جعفریه", StateId = 19, CountyId = 897 },
                new County { CountyName = "قنوات", StateId = 19, CountyId = 898 },
                new County { CountyName = "دستجرد", StateId = 19, CountyId = 899 }

                );

            #endregion
            #region کردستان
            modelBuilder.Entity<State>().HasData(
                new State
                {
                    StateId = 20,
                    StateName = "کردستان"
                });
            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "قروه", StateId = 20, CountyId = 900 },
                new County { CountyName = "توپ آغاج", StateId = 20, CountyId = 901 },
                new County { CountyName = "سروآباد", StateId = 20, CountyId = 902 },
                new County { CountyName = "بوئین سفلی", StateId = 20, CountyId = 903 },
                new County { CountyName = "زرینه", StateId = 20, CountyId = 904 },
                new County { CountyName = "دلبران", StateId = 20, CountyId = 905 },
                new County { CountyName = "سنندج", StateId = 20, CountyId = 906 },
                new County { CountyName = "یاسوکند", StateId = 20, CountyId = 907 },
                new County { CountyName = "موچش", StateId = 20, CountyId = 908 },
                new County { CountyName = "بانه", StateId = 20, CountyId = 909 },
                new County { CountyName = "مریوان", StateId = 20, CountyId = 910 },
                new County { CountyName = "سریش آباد", StateId = 20, CountyId = 911 },
                new County { CountyName = "صاحب", StateId = 20, CountyId = 912 },
                new County { CountyName = "دهگلان", StateId = 20, CountyId = 913 },
                new County { CountyName = "بابارشانی", StateId = 20, CountyId = 914 },
                new County { CountyName = "دیواندره", StateId = 20, CountyId = 915 },
                new County { CountyName = "برده رشه", StateId = 20, CountyId = 916 },
                new County { CountyName = "شویشه", StateId = 20, CountyId = 917 },
                new County { CountyName = "بیجار", StateId = 20, CountyId = 918 },
                new County { CountyName = "اورامان تخت", StateId = 20, CountyId = 919 },
                new County { CountyName = "کانی سور", StateId = 20, CountyId = 920 },
                new County { CountyName = "کانی دینار", StateId = 20, CountyId = 921 },
                new County { CountyName = "دزج", StateId = 20, CountyId = 922 },
                new County { CountyName = "سقز", StateId = 20, CountyId = 923 },
                new County { CountyName = "بلبان آباد", StateId = 20, CountyId = 924 },
                new County { CountyName = "پیرتاج", StateId = 20, CountyId = 925 },
                new County { CountyName = "کامیاران", StateId = 20, CountyId = 926 },
                new County { CountyName = "آرمرده", StateId = 20, CountyId = 927 },
                new County { CountyName = "چناره", StateId = 20, CountyId = 928 }

                );

            #endregion
            #region کرمان
            modelBuilder.Entity<State>().HasData(
               new State
               {
                   StateId = 21,
                   StateName = "کرمان"
               });
            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "کهنوج", StateId = 21, CountyId = 929 },
                new County { CountyName = "بلوک", StateId = 21, CountyId = 930 },
                new County { CountyName = "پاریز", StateId = 21, CountyId = 931 },
                new County { CountyName = "گنبکی", StateId = 21, CountyId = 932 },
                new County { CountyName = "زنگی آباد", StateId = 21, CountyId = 933 },
                new County { CountyName = "بم", StateId = 21, CountyId = 934 },
                new County { CountyName = "خانوک", StateId = 21, CountyId = 935 },
                new County { CountyName = "کیانشهر", StateId = 21, CountyId = 936 },
                new County { CountyName = "جوپار", StateId = 21, CountyId = 937 },
                new County { CountyName = "عنبر آباد", StateId = 21, CountyId = 938 },
                new County { CountyName = "جوزم", StateId = 21, CountyId = 939 },
                new County { CountyName = "نظام شهر", StateId = 21, CountyId = 940 },
                new County { CountyName = "لاله زار", StateId = 21, CountyId = 941 },
                new County { CountyName = "کشکوئیه", StateId = 21, CountyId = 942 },
                new County { CountyName = "زیدآباد", StateId = 21, CountyId = 943 },
                new County { CountyName = "هنزا", StateId = 21, CountyId = 944 },
                new County { CountyName = "چترود", StateId = 21, CountyId = 945 },
                new County { CountyName = "جبالبارز", StateId = 21, CountyId = 946 },
                new County { CountyName = "سیرجان", StateId = 21, CountyId = 947 },
                new County { CountyName = "رودبار", StateId = 21, CountyId = 948 },
                new County { CountyName = "کرمان", StateId = 21, CountyId = 949 },
                new County { CountyName = "بافت", StateId = 21, CountyId = 950 },
                new County { CountyName = "صفائیه", StateId = 21, CountyId = 951 },
                new County { CountyName = "منوجان", StateId = 21, CountyId = 952 },
                new County { CountyName = "اندوهجرد", StateId = 21, CountyId = 953 },
                new County { CountyName = "هجدک", StateId = 21, CountyId = 954 },
                new County { CountyName = "خورسند", StateId = 21, CountyId = 955 },
                new County { CountyName = "امین شهر", StateId = 21, CountyId = 956 },
                new County { CountyName = "بردسیر", StateId = 21, CountyId = 957 },
                new County { CountyName = "رفسنجان", StateId = 21, CountyId = 958 },
                new County { CountyName = "هماشهر", StateId = 21, CountyId = 959 },
                new County { CountyName = "محمد آباد", StateId = 21, CountyId = 960 },
                new County { CountyName = "اختیار آباد", StateId = 21, CountyId = 961 },
                new County { CountyName = "بروات", StateId = 21, CountyId = 962 },
                new County { CountyName = "ریحان", StateId = 21, CountyId = 963 },
                new County { CountyName = "کوهبنان", StateId = 21, CountyId = 964 },
                new County { CountyName = "ماهان", StateId = 21, CountyId = 965 },
                new County { CountyName = "دوساری", StateId = 21, CountyId = 966 },
                new County { CountyName = "دهج", StateId = 21, CountyId = 967 },
                new County { CountyName = "فاریاب", StateId = 21, CountyId = 968 },
                new County { CountyName = "گلزار", StateId = 21, CountyId = 969 },
                new County { CountyName = "بهرمان", StateId = 21, CountyId = 970 },
                new County { CountyName = "بلورد", StateId = 21, CountyId = 971 },
                new County { CountyName = "فهرج", StateId = 21, CountyId = 972 },
                new County { CountyName = "کاظم آباد", StateId = 21, CountyId = 973 },
                new County { CountyName = "جیرفت", StateId = 21, CountyId = 974 },
                new County { CountyName = "نجف شهر", StateId = 21, CountyId = 975 },
                new County { CountyName = "قلعه گنج", StateId = 21, CountyId = 976 },
                new County { CountyName = "باغین", StateId = 21, CountyId = 977 },
                new County { CountyName = "بزنجان", StateId = 21, CountyId = 978 },
                new County { CountyName = "زرند", StateId = 21, CountyId = 979 },
                new County { CountyName = "نودژ", StateId = 21, CountyId = 980 },
                new County { CountyName = "گلباف", StateId = 21, CountyId = 981 },
                new County { CountyName = "راور", StateId = 21, CountyId = 982 },
                new County { CountyName = "خاتون آباد", StateId = 21, CountyId = 983 },
                new County { CountyName = "نرمالشیر", StateId = 21, CountyId = 984 },
                new County { CountyName = "دشتکار", StateId = 21, CountyId = 985 },
                new County { CountyName = "مس سرچسمه", StateId = 21, CountyId = 986 },
                new County { CountyName = "خواجو شهر", StateId = 21, CountyId = 987 },
                new County { CountyName = "رابر", StateId = 21, CountyId = 989 },
                new County { CountyName = "راین", StateId = 21, CountyId = 990 },
                new County { CountyName = "درب بهشت", StateId = 21, CountyId = 991 },
                new County { CountyName = "یزدان شهر", StateId = 21, CountyId = 992 },
                new County { CountyName = "زهکلوت", StateId = 21, CountyId = 993 },
                new County { CountyName = "محی آباد", StateId = 21, CountyId = 994 },
                new County { CountyName = "مردهک", StateId = 21, CountyId = 995 },
                new County { CountyName = "شهداد", StateId = 21, CountyId = 996 },
                new County { CountyName = "ارزوئیه", StateId = 21, CountyId = 997 },
                new County { CountyName = "نگار", StateId = 21, CountyId = 998 },
                new County { CountyName = "شهربابک", StateId = 21, CountyId = 999 },
                new County { CountyName = "انار", StateId = 21, CountyId = 1000 }
                );
            #endregion
            #region کرمانشاه
            modelBuilder.Entity<State>().HasData(
                new State
                {
                    StateId = 22,
                    StateName = "کرمانشاه"
                });
            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "سنقر", StateId = 22, CountyId = 1001 },
                new County { CountyName = "شاهو", StateId = 22, CountyId = 1002 },
                new County { CountyName = "بانوره", StateId = 22, CountyId = 1003 },
                new County { CountyName = "تازه آباد", StateId = 22, CountyId = 1004 },
                new County { CountyName = "هلشی", StateId = 22, CountyId = 1005 },
                new County { CountyName = "جوانرود", StateId = 22, CountyId = 1006 },
                new County { CountyName = "قصر شیرین", StateId = 22, CountyId = 1007 },
                new County { CountyName = "نوسود", StateId = 22, CountyId = 1008 },
                new County { CountyName = "کرند", StateId = 22, CountyId = 1009 },
                new County { CountyName = "کوزران", StateId = 22, CountyId = 1010 },
                new County { CountyName = "بیستون", StateId = 22, CountyId = 1011 },
                new County { CountyName = "حمیل", StateId = 22, CountyId = 1012 },
                new County { CountyName = "گیلانغرب", StateId = 22, CountyId = 1013 },
                new County { CountyName = "سطر", StateId = 22, CountyId = 1014 },
                new County { CountyName = "روانسر", StateId = 22, CountyId = 1015 },
                new County { CountyName = "پاوه", StateId = 22, CountyId = 1016 },
                new County { CountyName = "ازگله", StateId = 22, CountyId = 1017 },
                new County { CountyName = "کرمانشاه", StateId = 22, CountyId = 1018 },
                new County { CountyName = "میان راهان", StateId = 22, CountyId = 1019 },
                new County { CountyName = "کنگاور", StateId = 22, CountyId = 1020 },
                new County { CountyName = "سرپل ذهاب", StateId = 22, CountyId = 1021 },
                new County { CountyName = "ریجاب", StateId = 22, CountyId = 1022 },
                new County { CountyName = "باینگان", StateId = 22, CountyId = 1023 },
                new County { CountyName = "هرسین", StateId = 22, CountyId = 1024 },
                new County { CountyName = "اسلام آباد غرب", StateId = 22, CountyId = 1025 },
                new County { CountyName = "سرمست", StateId = 22, CountyId = 1026 },
                new County { CountyName = "سومار", StateId = 22, CountyId = 1027 },
                new County { CountyName = "نودشه", StateId = 22, CountyId = 1028 },
                new County { CountyName = "گهواره", StateId = 22, CountyId = 1029 },
                new County { CountyName = "رباط", StateId = 22, CountyId = 1030 },
                new County { CountyName = "صحنه", StateId = 22, CountyId = 1031 },
                new County { CountyName = "گودین", StateId = 22, CountyId = 1032 }

                );
            #endregion
            #region کهکیلویه و بویراحمد
            modelBuilder.Entity<State>().HasData(
                new State
                {
                    StateId = 23,
                    StateName = "کهکیلویه و بویراحمد"
                });
            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "گراب سفلی", StateId = 23, CountyId = 1033 },
                new County { CountyName = "لنده", StateId = 23, CountyId = 1034 },
                new County { CountyName = "سی سخت", StateId = 23, CountyId = 1035 },
                new County { CountyName = "دهدشت", StateId = 23, CountyId = 1036 },
                new County { CountyName = "یاسوج", StateId = 23, CountyId = 1037 },
                new County { CountyName = "سرفاریاب", StateId = 23, CountyId = 1038 },
                 new County { CountyName = "دوگنبدان", StateId = 23, CountyId = 1039 },
                new County { CountyName = "چیتاب", StateId = 23, CountyId = 1040 },
                new County { CountyName = "لیکک", StateId = 23, CountyId = 1041 },
                new County { CountyName = "دیشموک", StateId = 23, CountyId = 1042 },
                new County { CountyName = "مادوان", StateId = 23, CountyId = 1043 },
                new County { CountyName = "باشت", StateId = 23, CountyId = 1044 },
                 new County { CountyName = "پاتاوه", StateId = 23, CountyId = 1045 },
                new County { CountyName = "قلعه رئیسی", StateId = 23, CountyId = 1046 },
                new County { CountyName = "مارگون", StateId = 23, CountyId = 1047 },
                new County { CountyName = "چرام", StateId = 23, CountyId = 1048 },
                new County { CountyName = "سوق", StateId = 23, CountyId = 1049 }
                );
            #endregion
            #region گلستان
            modelBuilder.Entity<State>().HasData(
                new State
                {
                    StateId = 24,
                    StateName = "گلستان"
                });
            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "سیمین شهر", StateId = 24, CountyId = 1050 },
                new County { CountyName = "مزرعه", StateId = 24, CountyId = 1051 },
                new County { CountyName = "رامیان", StateId = 24, CountyId = 1052 },
                new County { CountyName = "فراغی", StateId = 24, CountyId = 1053 },
                new County { CountyName = "گنبد کاووس", StateId = 24, CountyId = 1054 },
                new County { CountyName = "کردکوی", StateId = 24, CountyId = 1055 },
                new County { CountyName = "مراوه", StateId = 24, CountyId = 1056 },
                new County { CountyName = "بندر ترکمن", StateId = 24, CountyId = 1057 },
                new County { CountyName = "نگین شهر", StateId = 24, CountyId = 1058 },
                new County { CountyName = "آق قلا", StateId = 24, CountyId = 1059 },
                new County { CountyName = "سرخنکلاته", StateId = 24, CountyId = 1060 },
                new County { CountyName = "گالیکش", StateId = 24, CountyId = 1061 },
                new County { CountyName = "سنگدوین", StateId = 24, CountyId = 1062 },
                new County { CountyName = "دلند", StateId = 24, CountyId = 1063 },
                new County { CountyName = "بندر گز", StateId = 24, CountyId = 1064 },
                new County { CountyName = "نوده خاندوز", StateId = 24, CountyId = 1065 },
                new County { CountyName = "مینو دشت", StateId = 24, CountyId = 1066 },
                new County { CountyName = "گرگان", StateId = 24, CountyId = 1067 },
                new County { CountyName = "گمیش تپه", StateId = 24, CountyId = 1068 },
                new County { CountyName = "علی آباد", StateId = 24, CountyId = 1069 },
                new County { CountyName = "خان ببین", StateId = 24, CountyId = 1070 },
                new County { CountyName = "کلاله", StateId = 24, CountyId = 1071 },
                new County { CountyName = "اینچه برون", StateId = 24, CountyId = 1072 },
                new County { CountyName = "فاضل آباد", StateId = 24, CountyId = 1073 },
                new County { CountyName = "تاتار علیا", StateId = 24, CountyId = 1074 },
                new County { CountyName = "نوکنده", StateId = 24, CountyId = 1075 },
                new County { CountyName = "آزاد شهر", StateId = 24, CountyId = 1076 },
                new County { CountyName = "انبار آلوم", StateId = 24, CountyId = 1077 },
                new County { CountyName = "جلین", StateId = 24, CountyId = 1078 }
                );
            #endregion
            #region گیلان
            modelBuilder.Entity<State>().HasData(
                new State
                {
                    StateId = 25,
                    StateName = "گیلان"
                });
            modelBuilder.Entity<County>().HasData(
                 new County { CountyName = "منجیل", StateId = 25, CountyId = 1079 },
                new County { CountyName = "شلمان", StateId = 25, CountyId = 1080 },
                new County { CountyName = "خشکبیجار", StateId = 25, CountyId = 1081 },
                new County { CountyName = "ماکلوان", StateId = 25, CountyId = 1082 },
                new County { CountyName = "سنگر", StateId = 25, CountyId = 1083 },
                new County { CountyName = "مرجقل", StateId = 25, CountyId = 1084 },
                new County { CountyName = "لیسار", StateId = 25, CountyId = 1085 },
                new County { CountyName = "رضوانشهر", StateId = 25, CountyId = 1086 },
                new County { CountyName = "رحیم آباد", StateId = 25, CountyId = 1087 },
                new County { CountyName = "لوندویل", StateId = 25, CountyId = 1088 },
                new County { CountyName = "احمد سرگوراب", StateId = 25, CountyId = 1089 },
                new County { CountyName = "لوشان", StateId = 25, CountyId = 1090 },
                new County { CountyName = "اطاقوار", StateId = 25, CountyId = 1091 },
                new County { CountyName = "لشت نشاء", StateId = 25, CountyId = 1092 },
                new County { CountyName = "فومن", StateId = 25, CountyId = 1093 },
                new County { CountyName = "چوبر", StateId = 25, CountyId = 1094 },
                new County { CountyName = "بازار جمعه", StateId = 25, CountyId = 1095 },
                new County { CountyName = "کلاچای", StateId = 25, CountyId = 1096 },
                new County { CountyName = "بندر انزلی", StateId = 25, CountyId = 1097 },
                new County { CountyName = "املش", StateId = 25, CountyId = 1098 },
                new County { CountyName = "رستم آباد", StateId = 25, CountyId = 1099 },
                new County { CountyName = "لاهیجان", StateId = 25, CountyId = 1100 },
                new County { CountyName = "توتکابن", StateId = 25, CountyId = 1101 },
                new County { CountyName = "لنگرود", StateId = 25, CountyId = 1102 },
                new County { CountyName = "کوچصفهان", StateId = 25, CountyId = 1103 },
                new County { CountyName = "صومعه سرا", StateId = 25, CountyId = 1104 },
                new County { CountyName = "اسالم", StateId = 25, CountyId = 1105 },
                new County { CountyName = "دیلمان", StateId = 25, CountyId = 1106 },
                new County { CountyName = "رودسر", StateId = 25, CountyId = 1107 },
                new County { CountyName = "کیاشهر", StateId = 25, CountyId = 1108 },
                new County { CountyName = "شفت", StateId = 25, CountyId = 1109 },
                new County { CountyName = "رودبار", StateId = 25, CountyId = 1110 },
                new County { CountyName = "کومله", StateId = 25, CountyId = 1111 },
                new County { CountyName = "رشت", StateId = 25, CountyId = 1112 },
                new County { CountyName = "ماسوله", StateId = 25, CountyId = 1113 },
                new County { CountyName = "خمام", StateId = 25, CountyId = 1114 },
                new County { CountyName = "ماسال", StateId = 25, CountyId = 1115 },
                new County { CountyName = "واجارگاه", StateId = 25, CountyId = 1116 },
                new County { CountyName = "هشتپر (تالش)", StateId = 25, CountyId = 1117 },
                new County { CountyName = "پره سر", StateId = 25, CountyId = 1118 },
                new County { CountyName = "بره سر", StateId = 25, CountyId = 1119 },
                new County { CountyName = "آستارا", StateId = 25, CountyId = 1120 },
                new County { CountyName = "رودبنه", StateId = 25, CountyId = 1121 },
                new County { CountyName = "جیرنده", StateId = 25, CountyId = 1122 },
                new County { CountyName = "چاف و چمخاله", StateId = 25, CountyId = 1123 },
                new County { CountyName = "لولمان", StateId = 25, CountyId = 1124 },
                new County { CountyName = "گوراب زرمیخ", StateId = 25, CountyId = 1125 },
                new County { CountyName = "حویق", StateId = 25, CountyId = 1126 },
                new County { CountyName = "سیاهکل", StateId = 25, CountyId = 1127 },
                new County { CountyName = "چابکسر", StateId = 25, CountyId = 1128 },
                new County { CountyName = "آستانه اشرفیه", StateId = 25, CountyId = 1129 },
                new County { CountyName = "رانکوه", StateId = 25, CountyId = 1130 }
                );
            #endregion
            #region لرستان
            modelBuilder.Entity<State>().HasData(
               new State
               {
                   StateId = 26,
                   StateName = "لرستان"
               });
            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "چالانچولان", StateId = 26, CountyId = 1131 },
                new County { CountyName = "بیران شهر", StateId = 26, CountyId = 1132 },
                new County { CountyName = "ویسیان", StateId = 26, CountyId = 1133 },
                new County { CountyName = "شول آباد", StateId = 26, CountyId = 1134 },
                new County { CountyName = "پلدختر", StateId = 26, CountyId = 1135 },
                new County { CountyName = "کوهدشت", StateId = 26, CountyId = 1136 },
                new County { CountyName = "هفت چشمه", StateId = 26, CountyId = 1137 },
                new County { CountyName = "بروجرد", StateId = 26, CountyId = 1138 },
                new County { CountyName = "الشتر", StateId = 26, CountyId = 1139 },
                new County { CountyName = "مومن آباد", StateId = 26, CountyId = 1140 },
                new County { CountyName = "دورود", StateId = 26, CountyId = 1141 },
                new County { CountyName = "زاغه", StateId = 26, CountyId = 1142 },
                new County { CountyName = "چقابل", StateId = 26, CountyId = 1143 },
                new County { CountyName = "الیگودرز", StateId = 26, CountyId = 1144 },
                new County { CountyName = "معمولان", StateId = 26, CountyId = 1145 },
                new County { CountyName = "کوهنانی", StateId = 26, CountyId = 1146 },
                new County { CountyName = "نورآباد", StateId = 26, CountyId = 1147 },
                new County { CountyName = "سپیددشت", StateId = 26, CountyId = 1148 },
                new County { CountyName = "سراب دوره", StateId = 26, CountyId = 1149 },
                new County { CountyName = "ازنا", StateId = 26, CountyId = 1150 },
                new County { CountyName = "گراب", StateId = 26, CountyId = 1151 },
                new County { CountyName = "خرم آباد", StateId = 26, CountyId = 1152 },
                new County { CountyName = "اشترینان", StateId = 26, CountyId = 1153 },
                new County { CountyName = "فیروز آباد", StateId = 26, CountyId = 1154 },
                new County { CountyName = "درب گنبد", StateId = 26, CountyId = 1155 }

                );
            #endregion
            #region مازندران
            modelBuilder.Entity<State>().HasData(
                new State
                {
                    StateId = 27,
                    StateName = "مازندران"
                });
            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "گلوگاه", StateId = 27, CountyId = 1156 },
                new County { CountyName = "پل سفید", StateId = 27, CountyId = 1157 },
                new County { CountyName = "دابودشت", StateId = 27, CountyId = 1158 },
                new County { CountyName = "چالوس", StateId = 27, CountyId = 1159 },
                new County { CountyName = "کیاسر", StateId = 27, CountyId = 1160 },
                new County { CountyName = "بهمنمیر", StateId = 27, CountyId = 1161 },
                new County { CountyName = "تنکابن", StateId = 27, CountyId = 1162 },
                new County { CountyName = "کلاردشت", StateId = 27, CountyId = 1163 },
                new County { CountyName = "ایزدشهر", StateId = 27, CountyId = 1164 },
                new County { CountyName = "گتاب", StateId = 27, CountyId = 1165 },
                new County { CountyName = "سلمان شهر", StateId = 27, CountyId = 1166 },
                new County { CountyName = "ارطه", StateId = 27, CountyId = 1167 },
                new County { CountyName = "امیرکلا", StateId = 27, CountyId = 1168 },
                new County { CountyName = "کوهی خیل", StateId = 27, CountyId = 1169 },
                new County { CountyName = "پایین هولار", StateId = 27, CountyId = 1170 },
                new County { CountyName = "گزنک", StateId = 27, CountyId = 1171 },
                new County { CountyName = "محمود آباد", StateId = 27, CountyId = 1172 },
                new County { CountyName = "رامسر", StateId = 27, CountyId = 1173 },
                new County { CountyName = "نوشهر", StateId = 27, CountyId = 1174 },
                new County { CountyName = "خلیل آباد", StateId = 27, CountyId = 1175 },
                new County { CountyName = "کیاکلا", StateId = 27, CountyId = 1176 },
                new County { CountyName = "نور", StateId = 27, CountyId = 1177 },
                new County { CountyName = "مرزیکلا", StateId = 27, CountyId = 1178 },
                new County { CountyName = "فریدونکنار", StateId = 27, CountyId = 1179 },
                new County { CountyName = "زیرآب", StateId = 27, CountyId = 1180 },
                new County { CountyName = "امامزاده عبدالله", StateId = 27, CountyId = 1181 },
                new County { CountyName = "هچیرود", StateId = 27, CountyId = 1182 },
                new County { CountyName = "فریم", StateId = 27, CountyId = 1183 },
                new County { CountyName = "هادی شهر", StateId = 27, CountyId = 1184 },
                new County { CountyName = "نشتارود", StateId = 27, CountyId = 1185 },
                new County { CountyName = "پول", StateId = 27, CountyId = 1186 },
                new County { CountyName = "بهشهر", StateId = 27, CountyId = 1187 },
                new County { CountyName = "کلارآباد", StateId = 27, CountyId = 1188 },
                new County { CountyName = "بلده", StateId = 27, CountyId = 1189 },
                new County { CountyName = "بابل", StateId = 27, CountyId = 1190 },
                new County { CountyName = "جویبار", StateId = 27, CountyId = 1191 },
                new County { CountyName = "آلاشت", StateId = 27, CountyId = 1192 },
                new County { CountyName = "آمل", StateId = 27, CountyId = 1193 },
                new County { CountyName = "نکا", StateId = 27, CountyId = 1194 },
                new County { CountyName = "کتالم و سادات شهر", StateId = 27, CountyId = 1195 },
                new County { CountyName = "بابلسر", StateId = 27, CountyId = 1196 },
                new County { CountyName = "شیرود", StateId = 27, CountyId = 1197 },
                new County { CountyName = "شیرگاه", StateId = 27, CountyId = 1198 },
                new County { CountyName = "رویان", StateId = 27, CountyId = 1199 },
                new County { CountyName = "زرگر محله", StateId = 27, CountyId = 1200 },
                new County { CountyName = "عباس آباد", StateId = 27, CountyId = 1201 },
                new County { CountyName = "قائم شهر", StateId = 27, CountyId = 1202 },
                new County { CountyName = "خوش رودپی", StateId = 27, CountyId = 1203 },
                new County { CountyName = "مرزن آباد", StateId = 27, CountyId = 1204 },
                new County { CountyName = "ساری", StateId = 27, CountyId = 1205 },
                new County { CountyName = "رینه", StateId = 27, CountyId = 1206 },
                new County { CountyName = "سرخرود", StateId = 27, CountyId = 1207 },
                new County { CountyName = "خرم آباد", StateId = 27, CountyId = 1208 },
                new County { CountyName = "کجور", StateId = 27, CountyId = 1209 },
                new County { CountyName = "رستمکلا", StateId = 27, CountyId = 1210 },
                new County { CountyName = "سورک", StateId = 27, CountyId = 1211 },
                new County { CountyName = "چمستان", StateId = 27, CountyId = 1212 }
                );
            #endregion
            #region مرکزی
            modelBuilder.Entity<State>().HasData(
                new State
                {
                    StateId = 28,
                    StateName = "مرکزی"
                });
            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "آستانه", StateId = 28, CountyId = 1213 },
                new County { CountyName = "خنجین", StateId = 28, CountyId = 1214 },
                new County { CountyName = "نراق", StateId = 28, CountyId = 1215 },
                new County { CountyName = "کمیجان", StateId = 28, CountyId = 1216 },
                new County { CountyName = "آشتیان", StateId = 28, CountyId = 1217 },
                new County { CountyName = "رازقان", StateId = 28, CountyId = 1218 },
                new County { CountyName = "مهاجران", StateId = 28, CountyId = 1219 },
                new County { CountyName = "غرق آباد", StateId = 28, CountyId = 1220 },
                new County { CountyName = "خنداب", StateId = 28, CountyId = 1221 },
                new County { CountyName = "قورچی باشی", StateId = 28, CountyId = 1222 },
                new County { CountyName = "خشکرود", StateId = 28, CountyId = 1223 },
                new County { CountyName = "ساروق", StateId = 28, CountyId = 1224 },
                new County { CountyName = "محلات", StateId = 28, CountyId = 1225 },
                new County { CountyName = "شازند", StateId = 28, CountyId = 1226 },
                new County { CountyName = "ساوه", StateId = 28, CountyId = 1227 },
                new County { CountyName = "میلاجرد", StateId = 28, CountyId = 1228 },
                new County { CountyName = "تفرش", StateId = 28, CountyId = 1229 },
                new County { CountyName = "زاویه", StateId = 28, CountyId = 1230 },
                new County { CountyName = "اراک", StateId = 28, CountyId = 1231 },
                new County { CountyName = "توره", StateId = 28, CountyId = 1232 },
                new County { CountyName = "نوبران", StateId = 28, CountyId = 1233 },
                new County { CountyName = "فرمهین", StateId = 28, CountyId = 1234 },
                new County { CountyName = "دلیجان", StateId = 28, CountyId = 1235 },
                new County { CountyName = "پرندک", StateId = 28, CountyId = 1236 },
                new County { CountyName = "کارچان", StateId = 28, CountyId = 1237 },
                new County { CountyName = "نیمور", StateId = 28, CountyId = 1238 },
                new County { CountyName = "هندودر", StateId = 28, CountyId = 1239 },
                new County { CountyName = "آوه", StateId = 28, CountyId = 1240 },
                new County { CountyName = "جاورسیان", StateId = 28, CountyId = 1241 },
                new County { CountyName = "خمین", StateId = 28, CountyId = 1242 },
                new County { CountyName = "مامونیه", StateId = 28, CountyId = 1243 },
                new County { CountyName = "داودآباد", StateId = 28, CountyId = 1244 },
                new County { CountyName = "شهباز", StateId = 28, CountyId = 1245 }

                );
            #endregion
            #region هرمزگان
            modelBuilder.Entity<State>().HasData(
                new State
                {
                    StateId = 29,
                    StateName = "هرمزگان"
                });
            modelBuilder.Entity<County>().HasData(

                new County { CountyName = "بیکاء", StateId = 29, CountyId = 1246 },
                new County { CountyName = "تیرور", StateId = 29, CountyId = 1247 },
                new County { CountyName = "گروک", StateId = 29, CountyId = 1248 },
                new County { CountyName = "قشم", StateId = 29, CountyId = 1249 },
                new County { CountyName = "کوشکنار", StateId = 29, CountyId = 1250 },
                new County { CountyName = "کیش", StateId = 29, CountyId = 1251 },
                new County { CountyName = "سرگز", StateId = 29, CountyId = 1252 },
                new County { CountyName = "بندرعباس", StateId = 29, CountyId = 1253 },
                new County { CountyName = "زیارتعلی", StateId = 29, CountyId = 1254 },
                new County { CountyName = "سندرک", StateId = 29, CountyId = 1255 },
                new County { CountyName = "کوهستک", StateId = 29, CountyId = 1256 },
                new County { CountyName = "لمزان", StateId = 29, CountyId = 1257 },
                new County { CountyName = "رویدر", StateId = 29, CountyId = 1258 },
                new County { CountyName = "قلعه قاضی", StateId = 29, CountyId = 1259 },
                new County { CountyName = "فارغان", StateId = 29, CountyId = 1260 },
                new County { CountyName = "ابوموسی", StateId = 29, CountyId = 1261 },
                new County { CountyName = "هشتبندی", StateId = 29, CountyId = 1262 },
                new County { CountyName = "سردشت", StateId = 29, CountyId = 1263 },
                new County { CountyName = "درگهان", StateId = 29, CountyId = 1264 },
                new County { CountyName = "پارسیان", StateId = 29, CountyId = 1265 },
                new County { CountyName = "کنگ", StateId = 29, CountyId = 1266 },
                new County { CountyName = "جناح", StateId = 29, CountyId = 1267 },
                new County { CountyName = "تازیان پایین", StateId = 29, CountyId = 1268 },
                new County { CountyName = "دهبازر", StateId = 29, CountyId = 1269 },
                new County { CountyName = "میناب", StateId = 29, CountyId = 1270 },
                new County { CountyName = "سیریک", StateId = 29, CountyId = 1271 },
                new County { CountyName = "سوزا", StateId = 29, CountyId = 1272 },
                new County { CountyName = "خمیر", StateId = 29, CountyId = 1273 },
                new County { CountyName = "چارک", StateId = 29, CountyId = 1274 },
                new County { CountyName = "حاجی آباد", StateId = 29, CountyId = 1275 },
                new County { CountyName = "فین", StateId = 29, CountyId = 1276 },
                new County { CountyName = "بندر جاسک", StateId = 29, CountyId = 1277 },
                new County { CountyName = "گوهران", StateId = 29, CountyId = 1278 },
                new County { CountyName = "هرمز", StateId = 29, CountyId = 1279 },
                new County { CountyName = "دشتی", StateId = 29, CountyId = 1280 },
                new County { CountyName = "بندر لنگه", StateId = 29, CountyId = 1281 },
                new County { CountyName = "بستک", StateId = 29, CountyId = 1282 },
                new County { CountyName = "تخت", StateId = 29, CountyId = 1283 }
                );
            #endregion
            #region همدان
            modelBuilder.Entity<State>().HasData(
                new State
                {
                    StateId = 30,
                    StateName = "همدان"
                });
            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "زنگنه", StateId = 30, CountyId = 1284 },
                new County { CountyName = "دمق", StateId = 30, CountyId = 1285 },
                new County { CountyName = "سرکان", StateId = 30, CountyId = 1286 },
                new County { CountyName = "آجین", StateId = 30, CountyId = 1287 },
                new County { CountyName = "جورقان", StateId = 30, CountyId = 1288 },
                new County { CountyName = "برزول", StateId = 30, CountyId = 1289 },
                new County { CountyName = "فامنین", StateId = 30, CountyId = 1290 },
                new County { CountyName = "سامن", StateId = 30, CountyId = 1291 },
                new County { CountyName = "بهار", StateId = 30, CountyId = 1292 },
                new County { CountyName = "فرسنج", StateId = 30, CountyId = 1293 },
                new County { CountyName = "شیرین سو", StateId = 30, CountyId = 1294 },
                new County { CountyName = "مریانج", StateId = 30, CountyId = 1295 },
                new County { CountyName = "فیروزان", StateId = 30, CountyId = 1296 },
                new County { CountyName = "قروه درجزین", StateId = 30, CountyId = 1297 },
                new County { CountyName = "ازندریان", StateId = 30, CountyId = 1298 },
                new County { CountyName = "لالجین", StateId = 30, CountyId = 1299 },
                new County { CountyName = "گل تپه", StateId = 30, CountyId = 1300 },
                new County { CountyName = "گیان", StateId = 30, CountyId = 1301 },
                new County { CountyName = "ملایر", StateId = 30, CountyId = 1302 },
                new County { CountyName = "صالح آباد", StateId = 30, CountyId = 1303 },
                new County { CountyName = "تویسرکان", StateId = 30, CountyId = 1304 },
                new County { CountyName = "اسدآباد", StateId = 30, CountyId = 1305 },
                new County { CountyName = "همدان", StateId = 30, CountyId = 1306 },
                new County { CountyName = "نهاوند", StateId = 30, CountyId = 1307 },
                new County { CountyName = "رزن", StateId = 30, CountyId = 1308 },
                new County { CountyName = "جوکار", StateId = 30, CountyId = 1309 },
                new County { CountyName = "مهاجران", StateId = 30, CountyId = 1310 },
                new County { CountyName = "کبودرآهنگ", StateId = 30, CountyId = 1311 },
                new County { CountyName = "قهاوند", StateId = 30, CountyId = 1312 }

                );
            #endregion
            #region یزد
            modelBuilder.Entity<State>().HasData(
                new State
                {
                    StateId = 31,
                    StateName = "یزد"
                });
            modelBuilder.Entity<County>().HasData(
                new County { CountyName = "مرودست", StateId = 31, CountyId = 1313 },
                new County { CountyName = "مهردشت", StateId = 31, CountyId = 1314 },
                new County { CountyName = "حمیدیا", StateId = 31, CountyId = 1315 },
                new County { CountyName = "تفت", StateId = 31, CountyId = 1316 },
                new County { CountyName = "اشکذر", StateId = 31, CountyId = 1317 },
                new County { CountyName = "ندوشن", StateId = 31, CountyId = 1318 },
                new County { CountyName = "یزد", StateId = 31, CountyId = 1319 },
                new County { CountyName = "عقدا", StateId = 31, CountyId = 1320 },
                new County { CountyName = "بهاباد", StateId = 31, CountyId = 1321 },
                new County { CountyName = "ابرکوه", StateId = 31, CountyId = 1322 },
                new County { CountyName = "زارچ", StateId = 31, CountyId = 1323 },
                new County { CountyName = "نیر", StateId = 31, CountyId = 1324 },
                new County { CountyName = "اردکان", StateId = 31, CountyId = 1325 },
                new County { CountyName = "هرات", StateId = 31, CountyId = 1326 },
                new County { CountyName = "بفروییه", StateId = 31, CountyId = 1327 },
                new County { CountyName = "شاهدیه", StateId = 31, CountyId = 1328 },
                new County { CountyName = "بافق", StateId = 31, CountyId = 1329 },
                new County { CountyName = "خضرآباد", StateId = 31, CountyId = 1330 },
                new County { CountyName = "میبد", StateId = 31, CountyId = 1331 },
                new County { CountyName = "مهریز", StateId = 31, CountyId = 1332 },
                new County { CountyName = "احمدآباد", StateId = 31, CountyId = 1333 }

                );
            #endregion
            #region Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleTitle = "مدیر سایت", RoleName = "Admin", ThirdPartyPercent = 0, LiabilityPercent = 0, LifePercent = 0, TravelPercent = 0, FirePercent = 0, CarBodyPercent = 0 },
                new Role { RoleId = 2, RoleTitle = "مشتری", RoleName = "Customer", ThirdPartyPercent = 0, LiabilityPercent = 0, LifePercent = 0, TravelPercent = 0, FirePercent = 0, CarBodyPercent = 0 },
                new Role { RoleId = 3, RoleTitle = "همکار فروش", RoleName = "Marketer", ThirdPartyPercent = 2, LiabilityPercent = 10, LifePercent = 7, TravelPercent = 10, FirePercent = 10, CarBodyPercent = 5 },
                new Role { RoleId = 4, RoleTitle = "اپراتور", RoleName = "Operator", ThirdPartyPercent = 0, LiabilityPercent = 0, LifePercent = 0, TravelPercent = 0, FirePercent = 0, CarBodyPercent = 0 },
                new Role { RoleId = 5, RoleTitle = "کارشناس فروش", RoleName = "SalesEx", ThirdPartyPercent = 3, LiabilityPercent = 15, LifePercent = 10, TravelPercent = 15, FirePercent = 15, CarBodyPercent = 9 },
                new Role { RoleId = 6, RoleTitle = "نماینده", RoleName = "Agent", ThirdPartyPercent = 4, LiabilityPercent = 17, LifePercent = 20, TravelPercent = 0, FirePercent = 17, CarBodyPercent = 10 },
                new Role { RoleId = 7, RoleTitle = "متقاضی", RoleName = "Applicant", ThirdPartyPercent = 0, LiabilityPercent = 0, LifePercent = 0, TravelPercent = 0, FirePercent = 0, CarBodyPercent = 0 }

            );
            #endregion
            #region Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "سرپرستی",
                    Family = "فروش",
                    Cellphone = "09123689294",
                    CountyId = 330,
                    Password = "farbod-1356",
                    RegisteredDate = DateTime.UtcNow.AddHours(3.5),
                    AgentCode = "3312",
                    SalesExCode = "3312",
                    ReferralCode = "3312",
                    IsActive = true,
                    Phone = "02128428533",
                    Sex = "مرد",
                    NC = "0000000000"
                },
                new User
                {
                    Id = 2,
                    Name = "system",
                    Family = "agent",
                    Cellphone = "09199190919",
                    CountyId = 330,
                    Password = "system-agent",
                    RegisteredDate = DateTime.UtcNow.AddHours(3.5),
                    AgentCode = "919919",
                    SalesExCode = "919919",
                    ReferralCode = "919919",
                    IsActive = true,
                    Phone = "02128428533",
                    Sex = "مرد"
                }
            );
            #endregion
            #region UserRoles
            modelBuilder.Entity<UserRole>().HasData(
               new UserRole { URId = 1, IsActive = true, RegisterDate = DateTime.Now, UserId = 1, RoleId = 1, ThirdPartyPercent = 0, LiabilityPercent = 0, LifePercent = 0, TravelPercent = 0, FirePercent = 0, CarBodyPercent = 0 }
               );
            #endregion
            #region Permissions
            modelBuilder.Entity<Permission>().HasData(
                new Permission { PermissionId = 1, ParentId = null, PermissionName = "management", PermissionTitle = "مدیریت" },

                new Permission { PermissionId = 2, ParentId = 1, PermissionName = "upload", PermissionTitle = "آپلود" },

                new Permission { PermissionId = 3, ParentId = 2, PermissionName = "uploadcenter", PermissionTitle = "آپلود سنتر" },
                new Permission { PermissionId = 285, ParentId = 3, PermissionName = "addfileuc", PermissionTitle = "آپلود فایل" },
                new Permission { PermissionId = 286, ParentId = 3, PermissionName = "editfileuc", PermissionTitle = "ویرایش فایل" },
                new Permission { PermissionId = 287, ParentId = 3, PermissionName = "deletefileuc", PermissionTitle = "حذف فایل" },


                new Permission { PermissionId = 4, ParentId = 2, PermissionName = "ckuploadedfiles", PermissionTitle = "فایلهای آپلودی CK" },
                new Permission { PermissionId = 5, ParentId = 2, PermissionName = "uploadinsfile", PermissionTitle = "آپلود فایل بیمه" },

                new Permission { PermissionId = 6, ParentId = 1, PermissionName = "roles", PermissionTitle = "نقش ها" },
                new Permission { PermissionId = 7, ParentId = 6, PermissionName = "roleslist", PermissionTitle = "لیست نقش ها" },
                new Permission { PermissionId = 8, ParentId = 6, PermissionName = "addrole", PermissionTitle = "افزودن نقش" },
                new Permission { PermissionId = 9, ParentId = 6, PermissionName = "editrole", PermissionTitle = "ویرایش نقش" },
                new Permission { PermissionId = 10, ParentId = 6, PermissionName = "deleterole", PermissionTitle = "حذف نقش" },
                new Permission { PermissionId = 11, ParentId = 6, PermissionName = "detrole", PermissionTitle = "جزئیات نقش" },
                new Permission { PermissionId = 12, ParentId = 6, PermissionName = "roleper", PermissionTitle = "دسترسی" },

                new Permission { PermissionId = 13, ParentId = 1, PermissionName = "financial", PermissionTitle = "مالی" },
                new Permission { PermissionId = 14, ParentId = 13, PermissionName = "calcom", PermissionTitle = "محاسبه کارمزد" },
                new Permission { PermissionId = 15, ParentId = 13, PermissionName = "dfile", PermissionTitle = "دانلود فایل" },

                new Permission { PermissionId = 16, ParentId = 1, PermissionName = "insus", PermissionTitle = "بیمه ها" },

                new Permission { PermissionId = 17, ParentId = 16, PermissionName = "issuestate", PermissionTitle = "وضعیت صدور" },
                new Permission { PermissionId = 18, ParentId = 17, PermissionName = "addissuestate", PermissionTitle = "ثبت وضعیت صدور" },
                new Permission { PermissionId = 19, ParentId = 17, PermissionName = "deleteissuestate", PermissionTitle = "حذف وضعیت صدور" },
                new Permission { PermissionId = 20, ParentId = 17, PermissionName = "editissuestate", PermissionTitle = "ویرایش وضعیت صدور" },


                new Permission { PermissionId = 21, ParentId = 16, PermissionName = "financialstate", PermissionTitle = "وضعیت مالی" },
                new Permission { PermissionId = 22, ParentId = 21, PermissionName = "addfstate", PermissionTitle = "ثبت وضعیت مالی" },
                new Permission { PermissionId = 23, ParentId = 21, PermissionName = "deletefstate", PermissionTitle = "حذف وضعیت مالی" },
                new Permission { PermissionId = 24, ParentId = 21, PermissionName = "editfstate", PermissionTitle = "ویرایش وضعیت مالی" },

                new Permission { PermissionId = 25, ParentId = 16, PermissionName = "tpins", PermissionTitle = "بیمه ثالث" },

                new Permission { PermissionId = 26, ParentId = 25, PermissionName = "tpbaseinfo", PermissionTitle = "اطلاعات پایه" },
                new Permission { PermissionId = 27, ParentId = 26, PermissionName = "addtbi", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 28, ParentId = 26, PermissionName = "edittbi", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 29, ParentId = 26, PermissionName = "deletetbi", PermissionTitle = "حــذف" },

                new Permission { PermissionId = 30, ParentId = 25, PermissionName = "financialdamges", PermissionTitle = "جریمه خسارت مالی" },
                new Permission { PermissionId = 31, ParentId = 30, PermissionName = "addfd", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 32, ParentId = 30, PermissionName = "editfd", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 33, ParentId = 30, PermissionName = "deletefd", PermissionTitle = "حــذف" },

                new Permission { PermissionId = 34, ParentId = 25, PermissionName = "financialpremiums", PermissionTitle = "پوشش مالی" },
                new Permission { PermissionId = 35, ParentId = 34, PermissionName = "addfp", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 36, ParentId = 34, PermissionName = "editfp", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 37, ParentId = 34, PermissionName = "deletefp", PermissionTitle = "حــذف" },

                new Permission { PermissionId = 38, ParentId = 25, PermissionName = "incidentcovers", PermissionTitle = "پوششهای حوادث" },
                new Permission { PermissionId = 39, ParentId = 38, PermissionName = "addic", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 40, ParentId = 38, PermissionName = "editic", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 41, ParentId = 38, PermissionName = "deleteic", PermissionTitle = "حــذف" },

                new Permission { PermissionId = 42, ParentId = 25, PermissionName = "legaldiscounts", PermissionTitle = "تخفیفات و اضافات قانونی" },
                new Permission { PermissionId = 43, ParentId = 42, PermissionName = "addld", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 44, ParentId = 42, PermissionName = "editld", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 45, ParentId = 42, PermissionName = "deleteld", PermissionTitle = "حــذف" },

                new Permission { PermissionId = 46, ParentId = 25, PermissionName = "looslifedamges", PermissionTitle = "جرایم خسارت جانی" },
                new Permission { PermissionId = 47, ParentId = 46, PermissionName = "addlld", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 48, ParentId = 46, PermissionName = "editlld", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 49, ParentId = 46, PermissionName = "deletelld", PermissionTitle = "حــذف" },

                new Permission { PermissionId = 50, ParentId = 25, PermissionName = "vehicleusages", PermissionTitle = "کاربری های خودرو" },
                new Permission { PermissionId = 51, ParentId = 50, PermissionName = "addlvu", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 52, ParentId = 50, PermissionName = "editlvu", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 53, ParentId = 50, PermissionName = "deletelvu", PermissionTitle = "حــذف" },

                new Permission { PermissionId = 54, ParentId = 25, PermissionName = "vehiclegroups", PermissionTitle = "گروه های وسیله نقلیه" },
                new Permission { PermissionId = 55, ParentId = 54, PermissionName = "addvg", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 56, ParentId = 54, PermissionName = "editvg", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 57, ParentId = 54, PermissionName = "deletevg", PermissionTitle = "حــذف" },
                new Permission { PermissionId = 58, ParentId = 54, PermissionName = "addveh", PermissionTitle = "ثبت وسیله نقلیه" },
                new Permission { PermissionId = 59, ParentId = 54, PermissionName = "editveh", PermissionTitle = "ویرایش وسیله نقلیه" },
                new Permission { PermissionId = 60, ParentId = 54, PermissionName = "deletelveh", PermissionTitle = "حذف وسیله نقلیه" },
                new Permission { PermissionId = 61, ParentId = 54, PermissionName = "manageusage", PermissionTitle = "مدیریت کاربری" },

                new Permission { PermissionId = 62, ParentId = 25, PermissionName = "loosdrivedamages", PermissionTitle = "جرایم خسارت حوادث راننده" },
                new Permission { PermissionId = 63, ParentId = 62, PermissionName = "addldrd", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 64, ParentId = 62, PermissionName = "editldrd", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 65, ParentId = 62, PermissionName = "deleteldrd", PermissionTitle = "حــذف" },

                new Permission { PermissionId = 66, ParentId = 25, PermissionName = "tpinsurertypes", PermissionTitle = "انواع بیمه گذار" },
                new Permission { PermissionId = 67, ParentId = 66, PermissionName = "addtpit", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 68, ParentId = 66, PermissionName = "edittpit", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 69, ParentId = 66, PermissionName = "deletetpit", PermissionTitle = "حــذف" },

                new Permission { PermissionId = 70, ParentId = 16, PermissionName = "lifeins", PermissionTitle = "بیمه زندگی" },

                new Permission { PermissionId = 71, ParentId = 70, PermissionName = "paymentmethods", PermissionTitle = "روشهای پرداخت" },
                new Permission { PermissionId = 72, ParentId = 71, PermissionName = "addlpm", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 73, ParentId = 71, PermissionName = "editlpm", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 74, ParentId = 71, PermissionName = "deletelpm", PermissionTitle = "حذف" },

                new Permission { PermissionId = 75, ParentId = 70, PermissionName = "lifeplans", PermissionTitle = "طرح های بیمه" },
                new Permission { PermissionId = 76, ParentId = 75, PermissionName = "addllifep", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 77, ParentId = 75, PermissionName = "editlifep", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 78, ParentId = 75, PermissionName = "deletelifep", PermissionTitle = "حذف" },
                new Permission { PermissionId = 79, ParentId = 75, PermissionName = "managepm", PermissionTitle = "مدیریت روشهای پرداخت" },

                new Permission { PermissionId = 80, ParentId = 16, PermissionName = "fireins", PermissionTitle = "بیمه آتش سوزی" },

                new Permission { PermissionId = 81, ParentId = 80, PermissionName = "fbaseinfo", PermissionTitle = "اطلاعات پایه" },
                new Permission { PermissionId = 82, ParentId = 81, PermissionName = "addfbi", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 83, ParentId = 81, PermissionName = "editfbi", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 84, ParentId = 81, PermissionName = "deletefbi", PermissionTitle = "حذف" },

                new Permission { PermissionId = 85, ParentId = 80, PermissionName = "buildingusage", PermissionTitle = "کاربری ملک" },
                new Permission { PermissionId = 86, ParentId = 85, PermissionName = "addbu", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 87, ParentId = 85, PermissionName = "editbu", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 88, ParentId = 85, PermissionName = "deletebu", PermissionTitle = "حذف" },
                new Permission { PermissionId = 89, ParentId = 85, PermissionName = "managecovers", PermissionTitle = "مدیریت پوشش ها" },
                new Permission { PermissionId = 90, ParentId = 85, PermissionName = "staterates", PermissionTitle = "نرخهای استانی" },
                new Permission { PermissionId = 91, ParentId = 85, PermissionName = "addstr", PermissionTitle = "ثبت نرخ" },
                new Permission { PermissionId = 92, ParentId = 85, PermissionName = "deletestr", PermissionTitle = "حذف نرخ" },

                new Permission { PermissionId = 93, ParentId = 80, PermissionName = "firecovers", PermissionTitle = "پوششهای بیمه آتش سوزی" },
                new Permission { PermissionId = 94, ParentId = 93, PermissionName = "addfico", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 95, ParentId = 93, PermissionName = "editfico", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 96, ParentId = 93, PermissionName = "deletefico", PermissionTitle = "حذف" },

                new Permission { PermissionId = 97, ParentId = 80, PermissionName = "fireinsurertypes", PermissionTitle = "انواع بیمه گذار" },
                new Permission { PermissionId = 98, ParentId = 97, PermissionName = "addfity", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 99, ParentId = 97, PermissionName = "editfity", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 100, ParentId = 97, PermissionName = "deletefity", PermissionTitle = "حذف" },

                new Permission { PermissionId = 101, ParentId = 80, PermissionName = "firelegaldiscount", PermissionTitle = "تخفیفات و اضافات" },
                new Permission { PermissionId = 102, ParentId = 101, PermissionName = "addfld", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 103, ParentId = 101, PermissionName = "editfld", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 104, ParentId = 101, PermissionName = "deletefld", PermissionTitle = "حذف" },


                new Permission { PermissionId = 105, ParentId = 80, PermissionName = "firestructuretypes", PermissionTitle = "نوع سازه" },
                new Permission { PermissionId = 106, ParentId = 105, PermissionName = "addfsty", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 107, ParentId = 105, PermissionName = "editfsty", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 108, ParentId = 105, PermissionName = "deletefsty", PermissionTitle = "حذف" },

                new Permission { PermissionId = 109, ParentId = 16, PermissionName = "cbins", PermissionTitle = "بیمه بدنه" },

                new Permission { PermissionId = 110, ParentId = 109, PermissionName = "cbinsgroup", PermissionTitle = "گروه خودرو" },
                new Permission { PermissionId = 111, ParentId = 110, PermissionName = "addcbg", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 112, ParentId = 110, PermissionName = "editcbg", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 113, ParentId = 110, PermissionName = "deletecbg", PermissionTitle = "حذف" },
                new Permission { PermissionId = 114, ParentId = 110, PermissionName = "addcbcar", PermissionTitle = "ثبت خودرو" },
                new Permission { PermissionId = 115, ParentId = 110, PermissionName = "editcbcar", PermissionTitle = "ویرایش خودرو" },
                new Permission { PermissionId = 278, ParentId = 110, PermissionName = "detcbcar", PermissionTitle = "جزئیات خودرو" },
                new Permission { PermissionId = 279, ParentId = 110, PermissionName = "deletecbcar", PermissionTitle = "حذف خودرو" },
                new Permission { PermissionId = 280, ParentId = 110, PermissionName = "viewcbcars", PermissionTitle = "مشاهده خودروها" },


                new Permission { PermissionId = 116, ParentId = 109, PermissionName = "cbusage", PermissionTitle = "کاربری خودرو" },
                new Permission { PermissionId = 117, ParentId = 116, PermissionName = "addcbu", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 118, ParentId = 116, PermissionName = "editcbu", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 119, ParentId = 116, PermissionName = "deletecbu", PermissionTitle = "حذف" },
                new Permission { PermissionId = 120, ParentId = 116, PermissionName = "selcbug", PermissionTitle = "انتخاب گروه" },

                new Permission { PermissionId = 121, ParentId = 109, PermissionName = "cblegdis", PermissionTitle = "تخفیف و اضافه نرخ" },
                new Permission { PermissionId = 122, ParentId = 121, PermissionName = "addcbld", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 123, ParentId = 121, PermissionName = "editcbld", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 124, ParentId = 121, PermissionName = "deletecbld", PermissionTitle = "حذف" },

                new Permission { PermissionId = 125, ParentId = 109, PermissionName = "cbinsurertypes", PermissionTitle = "انواع بیمه گذار" },
                new Permission { PermissionId = 126, ParentId = 125, PermissionName = "addcbit", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 127, ParentId = 125, PermissionName = "editcbit", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 128, ParentId = 125, PermissionName = "deletecbit", PermissionTitle = "حذف" },

                new Permission { PermissionId = 129, ParentId = 109, PermissionName = "cbinsurancetypes", PermissionTitle = "انواع بیمه نامه" },
                new Permission { PermissionId = 130, ParentId = 129, PermissionName = "addcbinty", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 131, ParentId = 129, PermissionName = "editcbinty", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 132, ParentId = 129, PermissionName = "deletecbinty", PermissionTitle = "حذف" },

                new Permission { PermissionId = 133, ParentId = 109, PermissionName = "cbcovers", PermissionTitle = "پوششهای بیمه بدنه" },
                new Permission { PermissionId = 134, ParentId = 133, PermissionName = "addcbc", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 135, ParentId = 133, PermissionName = "editcbc", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 136, ParentId = 133, PermissionName = "deletecbc", PermissionTitle = "حذف" },

                new Permission { PermissionId = 137, ParentId = 16, PermissionName = "travelins", PermissionTitle = "بیمه مسافرتی" },

                new Permission { PermissionId = 138, ParentId = 137, PermissionName = "tinsco", PermissionTitle = "بیمه گر" },
                new Permission { PermissionId = 139, ParentId = 138, PermissionName = "addtico", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 140, ParentId = 138, PermissionName = "edittico", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 141, ParentId = 138, PermissionName = "deletetico", PermissionTitle = "حذف" },

                new Permission { PermissionId = 142, ParentId = 137, PermissionName = "travelclass", PermissionTitle = "کلاس بیمه نامه" },
                new Permission { PermissionId = 143, ParentId = 142, PermissionName = "addtc", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 144, ParentId = 142, PermissionName = "edittc", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 145, ParentId = 142, PermissionName = "deletetc", PermissionTitle = "حذف" },

                new Permission { PermissionId = 146, ParentId = 137, PermissionName = "travelzooms", PermissionTitle = "مناطق سفر" },
                new Permission { PermissionId = 147, ParentId = 146, PermissionName = "addtz", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 148, ParentId = 146, PermissionName = "edittz", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 149, ParentId = 146, PermissionName = "deletetz", PermissionTitle = "حذف" },

                new Permission { PermissionId = 150, ParentId = 1, PermissionName = "weblog", PermissionTitle = "وبلاگ" },

                new Permission { PermissionId = 151, ParentId = 150, PermissionName = "webloggroups", PermissionTitle = "گروه های وبلاگ" },
                new Permission { PermissionId = 152, ParentId = 151, PermissionName = "addwg", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 153, ParentId = 151, PermissionName = "editwg", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 154, ParentId = 151, PermissionName = "deletewg", PermissionTitle = "حذف" },

                new Permission { PermissionId = 155, ParentId = 150, PermissionName = "blogs", PermissionTitle = "بلاگ ها" },
                new Permission { PermissionId = 156, ParentId = 155, PermissionName = "addblg", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 157, ParentId = 155, PermissionName = "editblg", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 158, ParentId = 155, PermissionName = "deleteblg", PermissionTitle = "حذف" },

                new Permission { PermissionId = 159, ParentId = 1, PermissionName = "addinfos", PermissionTitle = "اطلاعات تکمیلی" },

                new Permission { PermissionId = 160, ParentId = 159, PermissionName = "abouts", PermissionTitle = "درباره ما" },
                new Permission { PermissionId = 161, ParentId = 160, PermissionName = "addabout", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 162, ParentId = 160, PermissionName = "editabout", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 163, ParentId = 160, PermissionName = "deleteabout", PermissionTitle = "حذف" },

                new Permission { PermissionId = 164, ParentId = 159, PermissionName = "usermessages", PermissionTitle = "پیامهای کاربران" },
                new Permission { PermissionId = 165, ParentId = 164, PermissionName = "umdet", PermissionTitle = "جزئیات" },

                new Permission { PermissionId = 166, ParentId = 159, PermissionName = "sliders", PermissionTitle = "اسلایدر" },
                new Permission { PermissionId = 167, ParentId = 166, PermissionName = "addslider", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 168, ParentId = 166, PermissionName = "editslider", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 169, ParentId = 166, PermissionName = "deleteslider", PermissionTitle = "حذف" },

                new Permission { PermissionId = 170, ParentId = 159, PermissionName = "specialoffer", PermissionTitle = "پیشنهاد ویژه" },
                new Permission { PermissionId = 171, ParentId = 170, PermissionName = "addspo", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 172, ParentId = 170, PermissionName = "editspo", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 173, ParentId = 170, PermissionName = "deletespo", PermissionTitle = "حذف" },

                new Permission { PermissionId = 174, ParentId = 159, PermissionName = "userhelpinfo", PermissionTitle = "راهنمای کاربران" },
                new Permission { PermissionId = 175, ParentId = 174, PermissionName = "adduhp", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 176, ParentId = 174, PermissionName = "edituhp", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 177, ParentId = 174, PermissionName = "deleteuhp", PermissionTitle = "حذف" },

                new Permission { PermissionId = 178, ParentId = 159, PermissionName = "faqs", PermissionTitle = "پرسش و پاسخ" },
                new Permission { PermissionId = 179, ParentId = 178, PermissionName = "addfaq", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 180, ParentId = 178, PermissionName = "editfaq", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 181, ParentId = 178, PermissionName = "deletefaq", PermissionTitle = "حذف" },

                new Permission { PermissionId = 182, ParentId = 159, PermissionName = "websiteupdates", PermissionTitle = "بروزرسانی ها" },
                new Permission { PermissionId = 183, ParentId = 182, PermissionName = "addwup", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 184, ParentId = 182, PermissionName = "editwup", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 185, ParentId = 182, PermissionName = "deletewup", PermissionTitle = "حذف" },

                new Permission { PermissionId = 186, ParentId = 159, PermissionName = "workwiths", PermissionTitle = "همکاری با ما" },
                new Permission { PermissionId = 187, ParentId = 186, PermissionName = "addww", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 188, ParentId = 186, PermissionName = "editww", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 189, ParentId = 186, PermissionName = "deleteww", PermissionTitle = "حذف" },

                new Permission { PermissionId = 281, ParentId = 159, PermissionName = "counties", PermissionTitle = "شهرستانها" },
                new Permission { PermissionId = 282, ParentId = 281, PermissionName = "addcou", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 283, ParentId = 281, PermissionName = "editcou", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 284, ParentId = 281, PermissionName = "detcou", PermissionTitle = "جزئیات" },


                new Permission { PermissionId = 190, ParentId = 1, PermissionName = "reports", PermissionTitle = "گزارشات" },

                new Permission { PermissionId = 191, ParentId = 190, PermissionName = "repusers", PermissionTitle = "کاربران" },
                new Permission { PermissionId = 192, ParentId = 191, PermissionName = "adduser", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 193, ParentId = 191, PermissionName = "edituser", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 194, ParentId = 191, PermissionName = "deleteuser", PermissionTitle = "حذف" },
                new Permission { PermissionId = 195, ParentId = 191, PermissionName = "addroletouser", PermissionTitle = "افزودن نقش" },
                new Permission { PermissionId = 196, ParentId = 191, PermissionName = "detuser", PermissionTitle = "جزئیات" },

                new Permission { PermissionId = 197, ParentId = 190, PermissionName = "reqsworkwith", PermissionTitle = "درخواست های همکاری" },
                new Permission { PermissionId = 198, ParentId = 197, PermissionName = "editrww", PermissionTitle = "ویرایش" },

                new Permission { PermissionId = 199, ParentId = 190, PermissionName = "coworkers", PermissionTitle = "همکاران" },
                new Permission { PermissionId = 200, ParentId = 199, PermissionName = "addcwo", PermissionTitle = "ثبت" },
                new Permission { PermissionId = 201, ParentId = 199, PermissionName = "edicwo", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 202, ParentId = 199, PermissionName = "deletecwo", PermissionTitle = "حذف" },

                new Permission { PermissionId = 203, ParentId = 1, PermissionName = "registerdreqs", PermissionTitle = "درخواستهای ثبت شده" },
                new Permission { PermissionId = 204, ParentId = 203, PermissionName = "detregreq", PermissionTitle = "جزئیات" },
                new Permission { PermissionId = 205, ParentId = 203, PermissionName = "editregreq", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 206, ParentId = 203, PermissionName = "addissuestreq", PermissionTitle = "ثبت وضعیت صدور" },
                new Permission { PermissionId = 207, ParentId = 203, PermissionName = "addfinstreq", PermissionTitle = "ثبت وضعیت مالی" },
                new Permission { PermissionId = 208, ParentId = 203, PermissionName = "addissstcomreq", PermissionTitle = "ثبت یادداشت وضعیت صدور" },
                new Permission { PermissionId = 209, ParentId = 203, PermissionName = "editissstcomreq", PermissionTitle = "ویرایش یادداشت وضعیت صدور" },
                new Permission { PermissionId = 210, ParentId = 203, PermissionName = "deleteissstcomreq", PermissionTitle = "حذف یادداشت وضعیت صدور" },
                new Permission { PermissionId = 211, ParentId = 203, PermissionName = "addfinstcomreq", PermissionTitle = "ثبت یادداشت وضعیت مالی" },
                new Permission { PermissionId = 212, ParentId = 203, PermissionName = "editfinstcomreq", PermissionTitle = "ویرایش یادداشت وضعیت مالی" },
                new Permission { PermissionId = 213, ParentId = 203, PermissionName = "deletefinstcomreq", PermissionTitle = "حذف یادداشت وضعیت مالی" },
                new Permission { PermissionId = 214, ParentId = 203, PermissionName = "attfilereq", PermissionTitle = "پیوست فایل" },
                new Permission { PermissionId = 215, ParentId = 203, PermissionName = "dwonloadatfilesreq", PermissionTitle = "دانلود تمام فایلهای پیوست شده" },
                new Permission { PermissionId = 216, ParentId = 203, PermissionName = "downloadatfilereq", PermissionTitle = "دانلود فایل پیوست شده" },
                new Permission { PermissionId = 217, ParentId = 203, PermissionName = "deleteatfilereq", PermissionTitle = "حذف فایل پیوست شده" },
                new Permission { PermissionId = 218, ParentId = 203, PermissionName = "downloaddocsreq", PermissionTitle = "دانلود مدارک" },
                new Permission { PermissionId = 219, ParentId = 203, PermissionName = "payactionreq", PermissionTitle = "عملیات پرداخت" },

                new Permission { PermissionId = 220, ParentId = null, PermissionName = "myregisterdreqs", PermissionTitle = "درخواستهای من" },
                new Permission { PermissionId = 221, ParentId = 220, PermissionName = "mydetregreq", PermissionTitle = "جزئیات" },
                new Permission { PermissionId = 222, ParentId = 220, PermissionName = "myeditregreq", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 223, ParentId = 220, PermissionName = "myaddissuestreq", PermissionTitle = "ثبت وضعیت صدور" },
                new Permission { PermissionId = 224, ParentId = 220, PermissionName = "myaddfinstreq", PermissionTitle = "ثبت وضعیت مالی" },
                new Permission { PermissionId = 225, ParentId = 220, PermissionName = "myaddissstcomreq", PermissionTitle = "ثبت یادداشت وضعیت صدور" },
                new Permission { PermissionId = 226, ParentId = 220, PermissionName = "myeditissstcomreq", PermissionTitle = "ویرایش یادداشت وضعیت صدور" },
                new Permission { PermissionId = 227, ParentId = 220, PermissionName = "mydeleteissstcomreq", PermissionTitle = "حذف یادداشت وضعیت صدور" },
                new Permission { PermissionId = 228, ParentId = 220, PermissionName = "myaddfinstcomreq", PermissionTitle = "ثبت یادداشت وضعیت مالی" },
                new Permission { PermissionId = 229, ParentId = 220, PermissionName = "myeditfinstcomreq", PermissionTitle = "ویرایش یادداشت وضعیت مالی" },
                new Permission { PermissionId = 230, ParentId = 220, PermissionName = "mydeletefinstcomreq", PermissionTitle = "حذف یادداشت وضعیت مالی" },
                new Permission { PermissionId = 231, ParentId = 220, PermissionName = "myattfilereq", PermissionTitle = "پیوست فایل" },
                new Permission { PermissionId = 232, ParentId = 220, PermissionName = "mydwonloadatfilesreq", PermissionTitle = "دانلود تمام فایلهای پیوست شده" },
                new Permission { PermissionId = 233, ParentId = 220, PermissionName = "mydownloadatfilereq", PermissionTitle = "دانلود فایل پیوست شده" },
                new Permission { PermissionId = 234, ParentId = 220, PermissionName = "mydeleteatfilereq", PermissionTitle = "حذف فایل پیوست شده" },
                new Permission { PermissionId = 235, ParentId = 220, PermissionName = "mydownloaddocsreq", PermissionTitle = "دانلود مدارک" },
                new Permission { PermissionId = 236, ParentId = 220, PermissionName = "mypayactionreq", PermissionTitle = "عملیات پرداخت" },

                new Permission { PermissionId = 237, ParentId = null, PermissionName = "myregisterdinss", PermissionTitle = "بیمه های من" },
                new Permission { PermissionId = 238, ParentId = 237, PermissionName = "mydetregins", PermissionTitle = "جزئیات" },
                new Permission { PermissionId = 239, ParentId = 237, PermissionName = "myeditregins", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 240, ParentId = 237, PermissionName = "myaddissuestins", PermissionTitle = "ثبت وضعیت صدور" },
                new Permission { PermissionId = 241, ParentId = 237, PermissionName = "myaddfinstins", PermissionTitle = "ثبت وضعیت مالی" },
                new Permission { PermissionId = 242, ParentId = 237, PermissionName = "myaddissstcomins", PermissionTitle = "ثبت یادداشت وضعیت صدور" },
                new Permission { PermissionId = 243, ParentId = 237, PermissionName = "myeditissstcomins", PermissionTitle = "ویرایش یادداشت وضعیت صدور" },
                new Permission { PermissionId = 244, ParentId = 237, PermissionName = "mydeleteissstcomins", PermissionTitle = "حذف یادداشت وضعیت صدور" },
                new Permission { PermissionId = 245, ParentId = 237, PermissionName = "myaddfinstcomins", PermissionTitle = "ثبت یادداشت وضعیت مالی" },
                new Permission { PermissionId = 246, ParentId = 237, PermissionName = "myeditfinstcomins", PermissionTitle = "ویرایش یادداشت وضعیت مالی" },
                new Permission { PermissionId = 247, ParentId = 237, PermissionName = "mydeletefinstcomins", PermissionTitle = "حذف یادداشت وضعیت مالی" },
                new Permission { PermissionId = 248, ParentId = 237, PermissionName = "myattfileins", PermissionTitle = "پیوست فایل" },
                new Permission { PermissionId = 249, ParentId = 237, PermissionName = "mydwonloadatfilesins", PermissionTitle = "دانلود تمام فایلهای پیوست شده" },
                new Permission { PermissionId = 250, ParentId = 237, PermissionName = "mydownloadatfileins", PermissionTitle = "دانلود فایل پیوست شده" },
                new Permission { PermissionId = 251, ParentId = 237, PermissionName = "mydeleteatfileins", PermissionTitle = "حذف فایل پیوست شده" },
                new Permission { PermissionId = 252, ParentId = 237, PermissionName = "mydownloaddocsins", PermissionTitle = "دانلود مدارک" },
                new Permission { PermissionId = 253, ParentId = 237, PermissionName = "mypayactionins", PermissionTitle = "عملیات پرداخت" },

                new Permission { PermissionId = 254, ParentId = null, PermissionName = "myins", PermissionTitle = "گزارش فروش" },
                new Permission { PermissionId = 255, ParentId = 254, PermissionName = "detregmyins", PermissionTitle = "جزئیات" },
                new Permission { PermissionId = 256, ParentId = 254, PermissionName = "editregmyins", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 257, ParentId = 254, PermissionName = "addissuestmyins", PermissionTitle = "ثبت وضعیت صدور" },
                new Permission { PermissionId = 258, ParentId = 254, PermissionName = "addfinstmyins", PermissionTitle = "ثبت وضعیت مالی" },
                new Permission { PermissionId = 259, ParentId = 254, PermissionName = "addissstcommyins", PermissionTitle = "ثبت یادداشت وضعیت صدور" },
                new Permission { PermissionId = 260, ParentId = 254, PermissionName = "editissstcommyins", PermissionTitle = "ویرایش یادداشت وضعیت صدور" },
                new Permission { PermissionId = 261, ParentId = 254, PermissionName = "deleteissstcommyins", PermissionTitle = "حذف یادداشت وضعیت صدور" },
                new Permission { PermissionId = 262, ParentId = 254, PermissionName = "addfinstcommyins", PermissionTitle = "ثبت یادداشت وضعیت مالی" },
                new Permission { PermissionId = 263, ParentId = 254, PermissionName = "editfinstcommyins", PermissionTitle = "ویرایش یادداشت وضعیت مالی" },
                new Permission { PermissionId = 264, ParentId = 254, PermissionName = "deletefinstcommyins", PermissionTitle = "حذف یادداشت وضعیت مالی" },
                new Permission { PermissionId = 265, ParentId = 254, PermissionName = "attfilemyins", PermissionTitle = "پیوست فایل" },
                new Permission { PermissionId = 266, ParentId = 254, PermissionName = "dwonloadatfilesmyins", PermissionTitle = "دانلود تمام فایلهای پیوست شده" },
                new Permission { PermissionId = 267, ParentId = 254, PermissionName = "downloadatfilemyins", PermissionTitle = "دانلود فایل پیوست شده" },
                new Permission { PermissionId = 268, ParentId = 254, PermissionName = "deleteatfilemyins", PermissionTitle = "حذف فایل پیوست شده" },
                new Permission { PermissionId = 269, ParentId = 254, PermissionName = "downloaddocsmyins", PermissionTitle = "دانلود مدارک" },
                new Permission { PermissionId = 270, ParentId = 254, PermissionName = "payactionmyins", PermissionTitle = "عملیات پرداخت" },

                new Permission { PermissionId = 271, ParentId = null, PermissionName = "comrep", PermissionTitle = "گزارش کارمزد" },
                new Permission { PermissionId = 272, ParentId = 271, PermissionName = "mycommisisons", PermissionTitle = "کارمزد فروش" },
                new Permission { PermissionId = 273, ParentId = 271, PermissionName = "collectionreport", PermissionTitle = "کارمزد وصول" },

                new Permission { PermissionId = 274, ParentId = null, PermissionName = "conversations", PermissionTitle = "پیامهای داخلی" },
                new Permission { PermissionId = 275, ParentId = 274, PermissionName = "addconv", PermissionTitle = "افزودن پیام" },
                new Permission { PermissionId = 276, ParentId = 274, PermissionName = "detconv", PermissionTitle = "جزئیات پیام" },
                new Permission { PermissionId = 277, ParentId = 274, PermissionName = "delconv", PermissionTitle = "حذف پیام" },

                new Permission { PermissionId = 288, ParentId = 1, PermissionName = "registerdinss", PermissionTitle = "بیمه های صادر شده" },
                new Permission { PermissionId = 289, ParentId = 288, PermissionName = "detregins", PermissionTitle = "جزئیات" },
                new Permission { PermissionId = 290, ParentId = 288, PermissionName = "editregins", PermissionTitle = "ویرایش" },
                new Permission { PermissionId = 291, ParentId = 288, PermissionName = "addissuestins", PermissionTitle = "ثبت وضعیت صدور" },
                new Permission { PermissionId = 292, ParentId = 288, PermissionName = "addfinstins", PermissionTitle = "ثبت وضعیت مالی" },
                new Permission { PermissionId = 293, ParentId = 288, PermissionName = "addissstcomins", PermissionTitle = "ثبت یادداشت وضعیت صدور" },
                new Permission { PermissionId = 294, ParentId = 288, PermissionName = "editissstcomins", PermissionTitle = "ویرایش یادداشت وضعیت صدور" },
                new Permission { PermissionId = 295, ParentId = 288, PermissionName = "deleteissstcomins", PermissionTitle = "حذف یادداشت وضعیت صدور" },
                new Permission { PermissionId = 296, ParentId = 288, PermissionName = "addfinstcomins", PermissionTitle = "ثبت یادداشت وضعیت مالی" },
                new Permission { PermissionId = 297, ParentId = 288, PermissionName = "editfinstcomins", PermissionTitle = "ویرایش یادداشت وضعیت مالی" },
                new Permission { PermissionId = 298, ParentId = 288, PermissionName = "deletefinstcomins", PermissionTitle = "حذف یادداشت وضعیت مالی" },
                new Permission { PermissionId = 299, ParentId = 288, PermissionName = "attfileins", PermissionTitle = "پیوست فایل" },
                new Permission { PermissionId = 300, ParentId = 288, PermissionName = "dwonloadatfilesins", PermissionTitle = "دانلود تمام فایلهای پیوست شده" },
                new Permission { PermissionId = 301, ParentId = 288, PermissionName = "downloadatfileins", PermissionTitle = "دانلود فایل پیوست شده" },
                new Permission { PermissionId = 302, ParentId = 288, PermissionName = "deleteatfileins", PermissionTitle = "حذف فایل پیوست شده" },
                new Permission { PermissionId = 303, ParentId = 288, PermissionName = "downloaddocsins", PermissionTitle = "دانلود مدارک" },
                new Permission { PermissionId = 304, ParentId = 288, PermissionName = "payactionins", PermissionTitle = "عملیات پرداخت" },

                new Permission { PermissionId = 305, ParentId = null, PermissionName = "payconf", PermissionTitle = "تایید پرداخت" },
                new Permission { PermissionId = 306, ParentId = null, PermissionName = "attachfile", PermissionTitle = "پیوست فایل" }


                );
            #endregion
            #region RolePermission
            int loop = 1;

            //while (loop <= 306)
            //{
            //    modelBuilder.Entity<RolePermission>().HasData(
            //    new RolePermission { RP_Id = loop, PermissionId = loop, RoleId = 1 }
            //    );
            //    loop++;
            //}


            #endregion
            #region InsurerKind
            modelBuilder.Entity<InsurerType>().HasData(
                new InsurerType { Id = 1, Title = "معمولی" },
                new InsurerType { Id = 2, Title = "گروهی" },
                new InsurerType { Id = 3, Title = "صندوق بازنشستگی", DiscountPercent = 25, RemovePickupLimit = true, RemoveTheYearLimit = true },
                new InsurerType { Id = 4, Title = "تامین اجتماعی", DiscountPercent = 25, RemovePickupLimit = true, RemoveTheYearLimit = true },
                new InsurerType { Id = 5, Title = "بیمه گذار زندگی", DiscountPercent = 0, RemovePickupLimit = true, RemoveTheYearLimit = true }
            );
            #endregion
            #region LoosDriverDamages
            modelBuilder.Entity<LoosDriverDamage>().HasData(
                new LoosDriverDamage { Id = 1, DamageCount = 1, DamagePercent = 30, RegDate = DateTime.UtcNow.AddHours(3.5) },
                new LoosDriverDamage { Id = 2, DamageCount = 2, DamagePercent = 70, RegDate = DateTime.UtcNow.AddHours(3.5) },
                new LoosDriverDamage { Id = 3, DamageCount = 3, DamagePercent = 100, RegDate = DateTime.UtcNow.AddHours(3.5) }
                );
            #endregion LoosDriverDamages
            #region VehicleGroup
            modelBuilder.Entity<VehicleGroup>().HasData(
                new VehicleGroup { Id = 1, GroupTitle = "سواری", ParentId = null },
                new VehicleGroup { Id = 2, GroupTitle = "کامیون", ParentId = null, },
                new VehicleGroup { Id = 3, ParentId = 1, GroupTitle = "کمتر از 4 سیلندر", FinancialPremium = 50000, DelayedPenalty = 24700, GroupPremium = 19375000, VehicleConstructionYearLimit = 1388 },
                new VehicleGroup { Id = 4, ParentId = 1, GroupTitle = "پیکان، پراید، سپند", FinancialPremium = 50000, DelayedPenalty = 24700, GroupPremium = 22973000, VehicleConstructionYearLimit = 1388 },
                new VehicleGroup { Id = 5, ParentId = 1, GroupTitle = "سایر 4 سیلندرها", FinancialPremium = 50000, DelayedPenalty = 24700, GroupPremium = 26971000, VehicleConstructionYearLimit = 1388 }
                );

            #endregion VehicleGroup
            #region VehicleUsage
            modelBuilder.Entity<VehicleUsage>().HasData(
                new VehicleUsage { Id = 1, Usage = "شخصی", Rate = 4 },
                new VehicleUsage { Id = 2, Usage = "آژانس", Rate = 3 },
                new VehicleUsage { Id = 3, Usage = "آمبولانس", Rate = 2 }
                );
            #endregion VehicleUsage
            #region LoosLifeDamage
            modelBuilder.Entity<LoosLifeDamage>().HasData(
                new LoosLifeDamage { Id = 1, DamageCount = 1, DamagePercent = 30 },
                new LoosLifeDamage { Id = 2, DamageCount = 2, DamagePercent = 70 },
                new LoosLifeDamage { Id = 3, DamageCount = 3, DamagePercent = 100 }
                );
            #endregion LoosLifeDamage
            #region FinancialDamage
            modelBuilder.Entity<FinancialDamage>().HasData(
                new FinancialDamage { Id = 1, DamageCount = 1, DamagePercent = 20, RegDate = DateTime.UtcNow.AddHours(3.5) },
                new FinancialDamage { Id = 2, DamageCount = 2, DamagePercent = 30, RegDate = DateTime.UtcNow.AddHours(3.5) },
                new FinancialDamage { Id = 3, DamageCount = 3, DamagePercent = 40, RegDate = DateTime.UtcNow.AddHours(3.5) }
                );
            #endregion FinancialDamage
            #region LegalDiscount
            modelBuilder.Entity<LegalDiscount>().HasData(
                new LegalDiscount { Id = 1, Title = "تخفیف قانونی", Percent = (decimal)2.5, Type = "dis", State = true, RegDate = DateTime.UtcNow.AddHours(3.5) },
                new LegalDiscount { Id = 2, Title = "تخفیف گروهی", Percent = 10, Type = "dis", State = true, RegDate = DateTime.UtcNow.AddHours(3.5) },
                new LegalDiscount { Id = 3, Title = "اضافه نرخ حادثه ساز", Percent = 10, Type = "add", State = true, RegDate = DateTime.UtcNow.AddHours(3.5) }
                );
            #endregion LegalDiscount
            #region IncidentCover
            modelBuilder.Entity<IncidentCover>().HasData(
                new IncidentCover { Id = 1, LifeEventCoverage = 6400000000, DriverEventCoverage = 4800000000, DriverEventPremium = 3360000, RegDate = DateTime.UtcNow.AddHours(3.5) }
                );
            #endregion IncidentCover
            #region FinancialPremium
            modelBuilder.Entity<FinancialPremium>().HasData(
                new FinancialPremium { Id = 1, FinancialCoverage = 160000000, Premium = 350000, RegDate = DateTime.Now },
                new FinancialPremium { Id = 2, FinancialCoverage = 320000000, Premium = 700000, RegDate = DateTime.Now },
                new FinancialPremium { Id = 3, FinancialCoverage = 500000000, Premium = 1000000, RegDate = DateTime.Now }
                );
            #endregion FinancialPremium
            #region ThirdPartyBaseData
            modelBuilder.Entity<ThirdPartyBaseData>().HasData(
                new ThirdPartyBaseData { Id = 1, DriverAccidentPremium = 3000000, LegalDiscounts = 2.5f, VAT = 9m, RegDate = DateTime.UtcNow.AddHours(3.5), LegalDiscountPermit = false }
                );
            #endregion ThirdPartyBaseData
            #region InsStatus
            modelBuilder.Entity<InsStatus>().HasData(
                new InsStatus { Id = 1, Text = "دریافت شده", IsDefault = true, IsSystemic = true },
                new InsStatus { Id = 2, Text = "در دست اقدام", IsSystemic = true },
                new InsStatus { Id = 3, Text = "مدارک ناقص", IsSystemic = true },
                new InsStatus { Id = 4, Text = "در حال صدور", IsSystemic = true },
                new InsStatus { Id = 5, Text = "صادر شده", IsSystemic = true, IsEndofProcess = true, GetInsNo = true },
                new InsStatus { Id = 6, Text = "رد شده", IsSystemic = true, IsEndofProcess = true },
                new InsStatus { Id = 7, Text = "اعلام حق بیمه", IsSystemic = true, GetPeyment = true },
                new InsStatus { Id = 8, Text = "وضعیت ناقص", IsSystemic = true, Imperfect = true }
                );
            #endregion InsStatus
            #region FinancialStatus
            modelBuilder.Entity<FinancialStatus>().HasData(
                new FinancialStatus { Id = 1, Text = "پرداخت شده", IsSystemic = true, IsEndofProcess = true },
                new FinancialStatus { Id = 2, Text = "پرداخت نشده", IsSystemic = true, IsDefault = true },
                new FinancialStatus { Id = 3, Text = "کسر پرداخت", IsSystemic = true, IsEndofProcess = false, GetAmount = true }
                );
            #endregion FinancialStatus
            #region BlogGroup
            modelBuilder.Entity<BlogGroup>().HasData(
                new BlogGroup { BlogGroupId = 10, BlogGroupTitle = "دانستنی ها", BlogGroupEnTitle = "insurance-knowledge", BlogGroupIcon = "knowledge.svg", IsActive = true },
                new BlogGroup { BlogGroupId = 11, BlogGroupTitle = "آموزش", BlogGroupEnTitle = "insurance-learning", BlogGroupIcon = "learning.svg", IsActive = true },
                new BlogGroup { BlogGroupId = 12, BlogGroupTitle = "اطلاعیه ها", BlogGroupEnTitle = "insurance-notices", BlogGroupIcon = "info.svg", IsActive = true },
                new BlogGroup { BlogGroupId = 13, BlogGroupTitle = "بیمه ملت", BlogGroupEnTitle = "mellat-insurance", BlogGroupIcon = "mellat-insurance.svg", IsActive = true },
                new BlogGroup { BlogGroupId = 14, BlogGroupTitle = "صنعت بیمه", BlogGroupEnTitle = "insurance-industry", BlogGroupIcon = "insurance.svg", IsActive = true },
                new BlogGroup { BlogGroupId = 15, BlogGroupTitle = "خسارت", BlogGroupEnTitle = "insurance-damage", BlogGroupIcon = "damage.svg", IsActive = true }
            );
            #endregion
        }
    }
}
