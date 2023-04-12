using PharmacyWebAPI.DataAccess;
using PharmacyWebAPI.Models.Enums;

namespace PharmacyWebAPI.Extensions
{
    public class ApplicationDbInitializer
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public static async Task Seed(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            if (context is not null)
            {
                await context.Database.EnsureCreatedAsync();
                //Category
                if (!context.Categories.Any())
                {
                    await context.Categories.AddRangeAsync(new List<Category>()
                    {
                        new Category()
                        {
                            Name = "Pain Relief",
                            ImageURL = "https://www.example.com/pain-relief.png"
                        },
                        new Category()
                        {
                            Name = "Cardiovascular Health",
                            ImageURL = "https://www.example.com/cardiovascular-health.png"
                        },
                        new Category()
                        {
                            Name = "Mental Health",
                            ImageURL = "https://www.example.com/mental-health.png"
                        },
                        new Category()
                        {
                            Name = "Antibiotics",
                            ImageURL = "https://www.example.com/antibiotics.png"
                        },
                        new Category()
                        {
                            Name = "Digestive Health",
                            ImageURL = "https://www.example.com/digestive-health.png"
                        },
                        new Category()
                        {
                            Name = "Allergy / Sinus Health",
                            ImageURL = "https://www.example.com/allergy-sinus-health.png"
                        },
                        new Category()
                        {
                            Name = "Endocrine / Hormone Health",
                            ImageURL = "https://www.example.com/endocrine-hormone-health.png"
                        },
                        new Category()
                        {
                            Name = "Respiratory Health",
                            ImageURL = "https://www.example.com/respiratory-health.png"
                        },
                        new Category()
                        {
                            Name = "Dermatological Health",
                            ImageURL = "https://www.example.com/dermatological-health.png"
                        },
                        new Category()
                        {
                            Name = "Women's Health",
                            ImageURL = "https://www.example.com/womens-health.png"
                        },
                        new Category()
                        {
                            Name = "Men's Health",
                            ImageURL = "https://www.example.com/mens-health.png"
                        },
                        new Category()
                        {
                            Name = "Oncology",
                            ImageURL = "https://www.example.com/oncology.png"
                        },
                        new Category()
                        {
                            Name = "Pediatrics",
                            ImageURL = "https://www.example.com/pediatrics.png"
                        },
                        new Category()
                        {
                            Name = "Immunology",
                            ImageURL = "https://www.example.com/immunology.png"
                        },
                        new Category()
                        {
                            Name = "Musculoskeletal Health",
                            ImageURL = "https://www.example.com/musculoskeletal-health.png"
                        },
                        new Category()
                        {
                            Name = "Addiction Treatment",
                            ImageURL = "https://www.example.com/addiction-treatment.png"
                        },
                        new Category()
                        {
                            Name = "Ophthalmological Health",
                            ImageURL = "https://www.example.com/ophthalmological-health.png"
                        },
                        new Category()
                        {
                            Name = "Neurological Health",
                            ImageURL = "https://www.example.com/neurological-health.png"
                        },
                        new Category()
                        {
                            Name = "Gastrointestinal Health",
                            ImageURL = "https://www.example.com/gastrointestinal-health.png"
                        },
                        new Category()
                        {
                            Name = "Reproductive Health",
                            ImageURL = "https://www.example.com/reproductive-health.png"
                        },

                        new Category()
                        {
                            Name = "Cholesterol-Lowering Medication",
                            ImageURL = "https://example.com/cholesterol.jpg"
                        },
                        new Category()
                        {
                            Name = "Antidiabetic Medication",
                            ImageURL = "https://example.com/antidiabetic.jpg"
                        },
                        new Category()
                        {
                            Name = "Thyroid Hormone Replacement Medication",
                            ImageURL = "https://example.com/thyroid.jpg"
                        },
                        new Category()
                        {
                            Name = "Proton Pump Inhibitor (PPI)",
                            ImageURL = "https://example.com/ppi.jpg"
                        },

                        new Category()
                        {
                            Name = "Bronchodilator",
                            ImageURL = "https://example.com/bronchodilator.jpg"
                        },
                        new Category()
                        {
                            Name = "Analgesic and Antipyretic Medication",
                            ImageURL = "https://example.com/analgesic.jpg"
                        },
                        new Category()
                        {
                            Name = "Selective Serotonin Reuptake Inhibitor (SSRI) Antidepressant",
                            ImageURL = "https://example.com/ssri.jpg"
                        },
                        new Category()
                        {
                            Name = "Anticoagulant Medication",
                            ImageURL = "https://example.com/anticoagulant.jpg"
                        },
                        new Category()
                        {
                            Name = "Nonsteroidal Anti-Inflammatory Drug (NSAID)",
                            ImageURL = "https://example.com/anticoagulant.jpg"
                        },
                        new Category()
                        {
                            Name = "Allergy Relief",
                            ImageURL = "https://example.com/anticoagulant.jpg"
                        },
                    });
                    context.SaveChanges();
                }
                //Manufacturer
                if (!context.Manufacturers.Any())
                {
                    context.Manufacturers.AddRange(new List<Manufacturer>()
                    {
                        new Manufacturer()
                        {
                            Name = "Johnson & Johnson"
                        },
                        new Manufacturer()
                        {
                            Name = "Pfizer",
                        },
                        new Manufacturer()
                        {
                            Name = "Mylan",
                        },
                        new Manufacturer()
                        {
                            Name = "Apotex",
                        },
                        new Manufacturer()
                        {
                            Name = "Eli Lilly and Company",
                        },
                        new Manufacturer()
                        {
                            Name = "Sandoz",
                        },
                        new Manufacturer()
                        {
                            Name = "Teva Pharmaceuticals USA",
                        },
                        new Manufacturer()
                        {
                            Name = "Mallinckrodt Pharmaceuticals",
                        },
                        new Manufacturer()
                        {
                            Name = "Alembic Pharmaceuticals Limited",
                        },
                        new Manufacturer()
                        {
                            Name = "Greenstone LLC",
                        },
                        new Manufacturer()
                        {
                            Name = "Bristol-Myers Squibb",
                        },
                        new Manufacturer()
                        {
                            Name = "Teva Pharmaceuticals USA",
                        },
                        new Manufacturer()
                        {
                            Name = "Watson Pharmaceuticals, Inc.",
                        },
                        new Manufacturer()
                        {
                            Name = "Ranbaxy Pharmaceuticals Inc.",
                        },
                        new Manufacturer()
                        {
                            Name = "Actavis Pharma, Inc."
                        },
                        new Manufacturer()
                        {
                            Name = "Amoxil"
                        },
                        new Manufacturer()
                        {
                            Name = "Losec"
                        },
                        new Manufacturer()
                        {
                            Name = "Levoxyl"
                        },
                        new Manufacturer()
                        {
                            Name = "Riomet"
                        },
                        new Manufacturer()
                        {
                            Name = "Zocor"
                        },
                        new Manufacturer()
                        {
                            Name = "Prinivil"
                        },
                        new Manufacturer()
                        {
                            Name = "Marevan"
                        },
                        new Manufacturer()
                        {
                            Name = "Celexa"
                        },
                        new Manufacturer()
                        {
                            Name = "Panadol"
                        },
                        new Manufacturer()
                        {
                            Name = "Proventil"
                        },
                        new Manufacturer()
                        {
                            Name = "Bayer"
                        }
                    });
                    context.SaveChanges();
                }
                //Drugs
                if (!context.Drugs.Any())
                {
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Amoxicillin",
                        DosageForm = DosageForm.Capsule,
                        DosageStrength = "500mg",
                        SideEffects = "nausea, vomiting, diarrhea, rash",
                        Contraindications = "allergy to penicillin or cephalosporins",
                        Description = "Amoxicillin is an antibiotic used to treat a wide range of bacterial infections.",
                        PregnancyCategory = PregnancyCategory.B,
                        ImageURL = "https://www.drugs.com/images/pills/fio/amoxicillin-500mg.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Antibiotics").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Amoxil").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Lisinopril",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "10mg",
                        SideEffects = "dizziness, headache, cough",
                        Contraindications = "history of angioedema, pregnancy",
                        Description = "Lisinopril is an angiotensin-converting enzyme (ACE) inhibitor used to treat high blood pressure and heart failure.",
                        PregnancyCategory = PregnancyCategory.D,
                        ImageURL = "https://www.drugs.com/images/pills/mmx/prinivil-10.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Cardiovascular Health").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Prinivil").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Simvastatin",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "20mg",
                        SideEffects = "muscle pain, weakness, headache",
                        Contraindications = "pregnancy, liver disease",
                        Description = "Simvastatin is a statin cholesterol-lowering medication used to reduce the risk of heart disease and stroke.",
                        PregnancyCategory = PregnancyCategory.X,
                        ImageURL = "https://www.drugs.com/images/pills/fio/simvastatin-20mg.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Cholesterol-Lowering Medication").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Zocor").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Metformin",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "500mg",
                        SideEffects = "nausea, diarrhea, stomach upset",
                        Contraindications = "kidney disease, liver disease, alcoholism",
                        Description = "Metformin is an antidiabetic medication used to treat type 2 diabetes.",
                        PregnancyCategory = PregnancyCategory.B,
                        ImageURL = "https://www.drugs.com/images/pills/mmx/metformin-hydrochloride-500mg.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Antidiabetic Medication").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Riomet").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Levothyroxine",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "50mcg",
                        SideEffects = "weight loss, tremors, headache",
                        Contraindications = "heart disease, thyrotoxicosis, adrenal gland problems",
                        Description = "Levothyroxine is a thyroid hormone replacement medication used to treat hypothyroidism.",
                        PregnancyCategory = PregnancyCategory.A,
                        ImageURL = "https://www.drugs.com/images/pills/mmx/synthroid-50.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Thyroid Hormone Replacement Medication").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Levoxyl").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Omeprazole",
                        DosageForm = DosageForm.Capsule,
                        DosageStrength = "20mg",
                        SideEffects = "headache, diarrhea, stomach pain",
                        Contraindications = "allergy to proton pump inhibitors, liver disease",
                        Description = "Omeprazole is a proton pump inhibitor (PPI) used to treat gastroesophageal reflux disease (GERD) and other acid-related conditions.",
                        PregnancyCategory = PregnancyCategory.C,
                        ImageURL = "https://www.drugs.com/images/pills/fio/omeprazole-20mg.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Proton Pump Inhibitor (PPI)").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Losec").Id,
                    })
                    ;
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Aspirin",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "325mg",
                        SideEffects = "stomach upset, heartburn, bleeding",
                        Contraindications = "allergy to aspirin, bleeding disorders, pregnancy",
                        Description = "Aspirin is a nonsteroidal anti-inflammatory drug (NSAID) used to treat pain, fever, and inflammation. It is also used to prevent blood clots and reduce the risk of heart attack and stroke.",
                        PregnancyCategory = PregnancyCategory.D,
                        ImageURL = "https://www.drugs.com/images/pills/fio/aspirin-325mg.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Nonsteroidal Anti-Inflammatory Drug (NSAID)").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Bayer").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Albuterol",
                        DosageForm = DosageForm.Inhaler,
                        DosageStrength = "90mcg per actuation",
                        SideEffects = "tremors, headache, nervousness",
                        Contraindications = "heart disease, high blood pressure, diabetes",
                        Description = "Albuterol is a bronchodilator used to treat asthma and other breathing disorders.",
                        PregnancyCategory = PregnancyCategory.C,
                        ImageURL = "https://www.drugs.com/images/pills/fio/albuterol-sulfate-90.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Bronchodilator").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Proventil").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Paracetamol",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "500mg",
                        SideEffects = "nausea, stomach pain, liver damage",
                        Contraindications = "alcoholism, liver disease",
                        Description = "Paracetamol is an analgesic and antipyretic medication used to treat mild to moderate pain and fever.",
                        PregnancyCategory = PregnancyCategory.B,
                        ImageURL = "https://www.drugs.com/images/pills/fio/paracetamol-500mg.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Analgesic and Antipyretic Medication").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Panadol").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Citalopram",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "20mg",
                        SideEffects = "nausea, dry mouth, drowsiness",
                        Contraindications = "heart disease, history of seizures, use of MAO inhibitors",
                        Description = "Citalopram is a selective serotonin reuptake inhibitor (SSRI) antidepressant used to treat depression and anxiety disorders.",
                        PregnancyCategory = PregnancyCategory.C,
                        ImageURL = "https://www.drugs.com/images/pills/fio/citalopram-20mg.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Selective Serotonin Reuptake Inhibitor (SSRI) Antidepressant").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Celexa").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Warfarin",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "5mg",
                        SideEffects = "bleeding, bruising, stomach pain",
                        Contraindications = "pregnancy, bleeding disorders, liver disease",
                        Description = "Warfarin is an anticoagulant medication used to prevent blood clots and reduce the risk of stroke and heart attack.",
                        PregnancyCategory = PregnancyCategory.X,
                        ImageURL = "https://www.drugs.com/images/pills/fio/warfarin-sodium-5mg.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Anticoagulant Medication").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Marevan").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Acetaminophen",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "500mg",
                        SideEffects = "nausea, stomach pain, rash",
                        Contraindications = "liver disease, alcoholism, G6PD deficiency",
                        Description = "Acetaminophen (also known as paracetamol) is a pain reliever and fever reducer.",
                        PregnancyCategory = PregnancyCategory.B,
                        ImageURL = "https://www.drugs.com/images/pills/mcneil-consumer/44-104.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Pain Relief").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Johnson & Johnson").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Loratadine",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "10mg",
                        SideEffects = "dry mouth, headache, drowsiness",
                        Contraindications = "kidney disease, liver disease, pregnancy",
                        Description = "Loratadine is an antihistamine used to treat allergies.",
                        PregnancyCategory = PregnancyCategory.B,
                        ImageURL = "https://www.drugs.com/images/pills/ultrahealth/ultrahealth-10.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Allergy Relief").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Pfizer").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Metoprolol",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "50mg",
                        SideEffects = "dizziness, fatigue, depression",
                        Contraindications = "heart failure, asthma, diabetes",
                        Description = "Metoprolol is a beta-blocker used to treat high blood pressure and heart conditions.",
                        PregnancyCategory = PregnancyCategory.C,
                        ImageURL = "https://www.drugs.com/images/pills/myl/512-50.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Cardiovascular Health").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Mylan").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Ciprofloxacin",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "500mg",
                        SideEffects = "nausea, diarrhea, headache",
                        Contraindications = "tendon problems, myasthenia gravis, pregnancy",
                        Description = "Ciprofloxacin is an antibiotic used to treat bacterial infections.",
                        PregnancyCategory = PregnancyCategory.C,
                        ImageURL = "https://www.drugs.com/images/pills/apo/AC500-1.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Antibiotics").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Apotex").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Tadalafil",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "20mg",
                        SideEffects = "headache, back pain, muscle aches",
                        Contraindications = "heart disease, low blood pressure, stroke",
                        Description = "Tadalafil is a phosphodiesterase-5 inhibitor used to treat erectile dysfunction and pulmonary arterial hypertension.",
                        PregnancyCategory = PregnancyCategory.B,
                        ImageURL = "https://www.drugs.com/images/pills/mfr/t20-1.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Men's Health").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Eli Lilly and Company").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Sertraline",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "100mg",
                        SideEffects = "nausea, diarrhea, insomnia",
                        Contraindications = "mania, liver disease, bipolar disorder",
                        Description = "Sertraline is a selective serotonin reuptake inhibitor (SSRI) used to treat depression, anxiety, and obsessive-compulsive disorder.",
                        PregnancyCategory = PregnancyCategory.C,
                        ImageURL = "https://www.drugs.com/images/pills/gg/GG-249-1.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Mental Health").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Greenstone LLC").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Hydrochlorothiazide",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "25mg",
                        SideEffects = "dizziness, headache, dry mouth",
                        Contraindications = "liver disease, diabetes, gout",
                        Description = "Hydrochlorothiazide is a thiazide diuretic used to treat high blood pressure and fluid retention.",
                        PregnancyCategory = PregnancyCategory.B,
                        ImageURL = "https://www.drugs.com/images/pills/gg/GG-165-1.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Cardiovascular Health").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Sandoz").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Amoxicillin",
                        DosageForm = DosageForm.Capsule,
                        DosageStrength = "500mg",
                        SideEffects = "nausea, vomiting, diarrhea",
                        Contraindications = "mononucleosis, liver disease, asthma",
                        Description = "Amoxicillin is a penicillin antibiotic used to treat bacterial infections.",
                        PregnancyCategory = PregnancyCategory.B,
                        ImageURL = "https://www.drugs.com/images/pills/rx/rx829-1.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Antibiotics").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Ranbaxy Pharmaceuticals Inc.").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Rosuvastatin",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "10mg",
                        SideEffects = "muscle pain, diarrhea, headache",
                        Contraindications = "liver disease, pregnancy, breastfeeding",
                        Description = "Rosuvastatin is a statin used to lower cholesterol levels and prevent heart disease.",
                        PregnancyCategory = PregnancyCategory.X,
                        ImageURL = "https://www.drugs.com/images/pills/teva/0093-7178-01.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Cardiovascular Health").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Teva Pharmaceuticals USA").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Levothyroxine",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "50mcg",
                        SideEffects = "hair loss, weight loss, insomnia",
                        Contraindications = "thyrotoxicosis, adrenal gland problems, heart disease",
                        Description = "Levothyroxine is a thyroid hormone replacement used to treat hypothyroidism and prevent goiter.",
                        PregnancyCategory = PregnancyCategory.A,
                        ImageURL = "https://www.drugs.com/images/pills/actavis/L-20.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Endocrine / Hormone Health").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Actavis Pharma, Inc.").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Ibuprofen",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "200mg",
                        SideEffects = "nausea, stomach pain, headache",
                        Contraindications = "asthma, pregnancy, bleeding disorder",
                        Description = "Ibuprofen is a nonsteroidal anti-inflammatory drug (NSAID) used to treat pain and fever.",
                        PregnancyCategory = PregnancyCategory.C,
                        ImageURL = "https://www.drugs.com/images/pills/mr/MR-23-1.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Pain Relief").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Mallinckrodt Pharmaceuticals").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Losartan",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "50mg",
                        SideEffects = "dizziness, headache, fatigue",
                        Contraindications = "pregnancy, liver disease, kidney disease",
                        Description = "Losartan is an angiotensin II receptor blocker (ARB) used to treat high blood pressure and protect kidneys from damage in diabetic patients.",
                        PregnancyCategory = PregnancyCategory.D,
                        ImageURL = "https://www.drugs.com/images/pills/rx/rx724-1.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Cardiovascular Health").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Alembic Pharmaceuticals Limited").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Alprazolam",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "0.5mg",
                        SideEffects = "drowsiness, dizziness, headache",
                        Contraindications = "glaucoma, liver disease, depression",
                        Description = "Alprazolam is a benzodiazepine used to treat anxiety and panic disorders.",
                        PregnancyCategory = PregnancyCategory.D,
                        ImageURL = "https://www.drugs.com/images/pills/rx/rx869-1.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Mental Health").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Greenstone LLC").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Doxycycline",
                        DosageForm = DosageForm.Capsule,
                        DosageStrength = "100mg",
                        SideEffects = "nausea, diarrhea, skin rash",
                        Contraindications = "pregnancy, children under 8 years old, liver disease",
                        Description = "Doxycycline is a tetracycline antibiotic used to treat bacterial infections.",
                        PregnancyCategory = PregnancyCategory.D,
                        ImageURL = "https://www.drugs.com/images/pills/bm/100-2.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Antibiotics").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Bristol-Myers Squibb").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Metformin",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "500mg",
                        SideEffects = "nausea, diarrhea, stomach upset",
                        Contraindications = "kidney disease, liver disease, alcohol use",
                        Description = "Metformin is an oral diabetes medicine used to control blood sugar levels.",
                        PregnancyCategory = PregnancyCategory.B,
                        ImageURL = "https://www.drugs.com/images/pills/actavis/142-1.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Endocrine / Hormone Health").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Actavis Pharma, Inc.").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Fluoxetine",
                        DosageForm = DosageForm.Capsule,
                        DosageStrength = "20mg",
                        SideEffects = "nausea, headache, insomnia",
                        Contraindications = "bipolar disorder, liver disease, seizures",
                        Description = "Fluoxetine is a selective serotonin reuptake inhibitor (SSRI) used to treat depression, anxiety, and obsessive-compulsive disorder.",
                        PregnancyCategory = PregnancyCategory.C,
                        ImageURL = "https://www.drugs.com/images/pills/rx/rx710-1.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Mental Health").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Ranbaxy Pharmaceuticals Inc.").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Omeprazole",
                        DosageForm = DosageForm.Capsule,
                        DosageStrength = "20mg",
                        SideEffects = "headache, nausea, diarrhea",
                        Contraindications = "liver disease, osteoporosis, low magnesium levels",
                        Description = "Omeprazole is a proton pump inhibitor (PPI) used to treat heartburn and acid reflux.",
                        PregnancyCategory = PregnancyCategory.C,
                        ImageURL = "https://www.drugs.com/images/pills/teva/TEVA-7635-20mg.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Digestive Health").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Teva Pharmaceuticals USA").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Tramadol",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "50mg",
                        SideEffects = "nausea, dizziness, constipation",
                        Contraindications = "severe asthma, liver disease, seizures",
                        Description = "Tramadol is a narcotic-like pain reliever used to treat moderate to severe pain.",
                        PregnancyCategory = PregnancyCategory.C,
                        ImageURL = "https://www.drugs.com/images/pills/rx/rx652-1.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Pain Relief").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Ranbaxy Pharmaceuticals Inc.").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Lisinopril",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "10mg",
                        SideEffects = "dizziness, headache, dry cough",
                        Contraindications = "pregnancy, kidney disease, liver disease",
                        Description = "Lisinopril is an angiotensin-converting enzyme (ACE) inhibitor used to treat high blood pressure and heart failure.",
                        PregnancyCategory = PregnancyCategory.D,
                        ImageURL = "https://www.drugs.com/images/pills/abg/ABG-10-1.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Cardiovascular Health").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Actavis Pharma, Inc.").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Cetirizine",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "10mg",
                        SideEffects = "drowsiness, dry mouth, headache",
                        Contraindications = "kidney disease, liver disease, pregnancy",
                        Description = "Cetirizine is an antihistamine used to treat allergies and hives.",
                        PregnancyCategory = PregnancyCategory.B,
                        ImageURL = "https://www.drugs.com/images/pills/actavis/003-2.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Allergy / Sinus Health").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Actavis Pharma, Inc.").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Morphine",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "15mg",
                        SideEffects = "drowsiness, constipation, nausea",
                        Contraindications = "breathing problems, head injury, liver disease",
                        Description = "Morphine is a narcotic pain reliever used to treat severe pain.",
                        PregnancyCategory = PregnancyCategory.C,
                        ImageURL = "https://www.drugs.com/images/pills/abg/ABG-15-1.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Pain Relief").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Actavis Pharma, Inc.").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Metoprolol",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "25mg",
                        SideEffects = "dizziness, fatigue, depression",
                        Contraindications = "heart block, asthma, liver disease",
                        Description = "Metoprolol is a beta blocker used to treat high blood pressure and prevent heart attacks.",
                        PregnancyCategory = PregnancyCategory.C,
                        ImageURL = "https://www.drugs.com/images/pills/rx/rx162-1.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Cardiovascular Health").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Ranbaxy Pharmaceuticals Inc.").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Ciprofloxacin",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "500mg",
                        SideEffects = "nausea, diarrhea, headache",
                        Contraindications = "pregnancy, children under 18 years old, liver disease",
                        Description = "Ciprofloxacin is a fluoroquinolone antibiotic used to treat bacterial infections.",
                        PregnancyCategory = PregnancyCategory.C,
                        ImageURL = "https://www.drugs.com/images/pills/rx/rx710-1.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Antibiotics").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Ranbaxy Pharmaceuticals Inc.").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Furosemide",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "40mg",
                        SideEffects = "dizziness, headache, muscle cramps",
                        Contraindications = "anuria, liver disease, diabetes",
                        Description = "Furosemide is a loop diuretic used to treat fluid retention and high blood pressure.",
                        PregnancyCategory = PregnancyCategory.C,
                        ImageURL = "https://www.drugs.com/images/pills/wpi/WPI-39-10.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Cardiovascular Health").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Watson Pharmaceuticals, Inc.").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Atorvastatin",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "20mg",
                        SideEffects = "muscle pain, diarrhea, headache",
                        Contraindications = "liver disease, pregnancy, breastfeeding",
                        Description = "Atorvastatin is a statin used to lower cholesterol levels and prevent heart disease.",
                        PregnancyCategory = PregnancyCategory.X,
                        ImageURL = "https://www.drugs.com/images/pills/rx/rx828-1.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Cardiovascular Health").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Ranbaxy Pharmaceuticals Inc.").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Sertraline",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "50mg",
                        SideEffects = "nausea, diarrhea, insomnia",
                        Contraindications = "bipolar disorder, liver disease, seizures",
                        Description = "Sertraline is a selective serotonin reuptake inhibitor (SSRI) used to treat depression, anxiety, and obsessive-compulsive disorder.",
                        PregnancyCategory = PregnancyCategory.C,
                        ImageURL = "https://www.drugs.com/images/pills/abg/ABG-50-1.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Mental Health").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Actavis Pharma, Inc.").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Lorazepam",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "1mg",
                        SideEffects = "drowsiness, dizziness, confusion",
                        Contraindications = "glaucoma, liver disease, respiratory depression",
                        Description = "Lorazepam is a benzodiazepine used to treat anxiety and insomnia.",
                        PregnancyCategory = PregnancyCategory.D,
                        ImageURL = "https://www.drugs.com/images/pills/rx/rx773-1.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Mental Health").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Ranbaxy Pharmaceuticals Inc.").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Levothyroxine",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "50mcg",
                        SideEffects = "weight loss, insomnia, headache",
                        Contraindications = "thyrotoxicosis, acute myocardial infarction, uncorrected adrenal insufficiency",
                        Description = "Levothyroxine is a thyroid hormone used to treat hypothyroidism and goiter.",
                        PregnancyCategory = PregnancyCategory.A,
                        ImageURL = "https://www.drugs.com/images/pills/rx/rx679-1.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Endocrine / Hormone Health").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Ranbaxy Pharmaceuticals Inc.").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Ibuprofen",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "200mg",
                        SideEffects = "stomach upset, headache, dizziness",
                        Contraindications = "asthma, kidney disease, heart disease",
                        Description = "Ibuprofen is a nonsteroidal anti-inflammatory drug (NSAID) used to treat pain and inflammation.",
                        PregnancyCategory = PregnancyCategory.C,
                        ImageURL = "https://www.drugs.com/images/pills/rx/rx704-1.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Pain Relief").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Ranbaxy Pharmaceuticals Inc.").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Propranolol",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "40mg",
                        SideEffects = "dizziness, fatigue, nausea",
                        Contraindications = "asthma, heart block, liver disease",
                        Description = "Propranolol is a beta blocker used to treat high blood pressure, heart rhythm disorders, and anxiety.",
                        PregnancyCategory = PregnancyCategory.C,
                        ImageURL = "https://www.drugs.com/images/pills/abg/ABG-40-1.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Cardiovascular Health").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Actavis Pharma, Inc.").Id,
                    });
                    context.Drugs.Add(new Drug()
                    {
                        Name = "Azithromycin",
                        DosageForm = DosageForm.Tablet,
                        DosageStrength = "250mg",
                        SideEffects = "nausea, diarrhea, stomach pain",
                        Contraindications = "liver disease, heart rhythm disorder, myasthenia gravis",
                        Description = "Azithromycin is a macrolide antibiotic used to treat bacterial infections.",
                        PregnancyCategory = PregnancyCategory.B,
                        ImageURL = "https://www.drugs.com/images/pills/rx/rx710-1.jpg",
                        CategoryId = context.Categories.FirstOrDefault(x => x.Name == "Antibiotics").Id,
                        ManufacturerId = context.Manufacturers.FirstOrDefault(x => x.Name == "Ranbaxy Pharmaceuticals Inc.").Id,
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}