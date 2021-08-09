namespace Diaries.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Diaries.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<Diaries.Models.DiariesDB>
    {
        static public DateTime DTMonday { get; set; }
        static public DateTime DTTuesday { get; set; }
        static public DateTime DTWednesday { get; set; }
        static public DateTime DTThursday { get; set; }
        static public DateTime DTFriday { get; set; }
        static public DateTime DTSaturday { get; set; }
        static public DateTime DTSunday { get; set; }
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Diaries.Models.DiariesDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            DateTime dt = Convert.ToDateTime("01/08/2014");

            int span = dt.DayOfWeek - DayOfWeek.Monday;
            int span1 = dt.DayOfWeek - DayOfWeek.Tuesday;
            int span2 = dt.DayOfWeek - DayOfWeek.Wednesday;
            int span3 = dt.DayOfWeek - DayOfWeek.Thursday;
            int span4 = dt.DayOfWeek - DayOfWeek.Friday;
            int span5 = dt.DayOfWeek - DayOfWeek.Saturday;
            int span6 = dt.DayOfWeek - DayOfWeek.Sunday;


            DTMonday = dt.AddDays(-span);
            DTTuesday = dt.AddDays(-span1);
            DTWednesday = dt.AddDays(-span2);
            DTThursday = dt.AddDays(-span3);
            DTFriday = dt.AddDays(-span4);
            DTSaturday = dt.AddDays(-span5);
            DTSunday = dt.AddDays(-span6);


            //context.tblDiary.AddOrUpdate(a => a.D_DiaryName,
            //             new D_Diaries { D_DiaryName = "All", D_id = 1, D_Bckground_Icon_Colour = "peachpuff" },
            //             new D_Diaries { D_DiaryName = "CiVil", D_id = 2, D_Bckground_Icon_Colour = "seagreen" },
            //             new D_Diaries { D_DiaryName = "Committals", D_id = 3, D_Bckground_Icon_Colour = "lightblue" },
            //             new D_Diaries { D_DiaryName = "County Court", D_id = 4, D_Bckground_Icon_Colour = "lightred" },
            //             new D_Diaries { D_DiaryName = "Criminal", D_id = 5, D_Bckground_Icon_Colour = "gray" },
            //             new D_Diaries { D_DiaryName = "VOCAT", D_id = 6, D_Bckground_Icon_Colour = "yellow" });

            //context.tblDiaryListingInfo.AddOrUpdate(b => b.D_id,

            //         new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 1, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CV1", V_H_DiaryListDetails_DateCreated = DTMonday, V_H_DiaryListDetails_Avail = 3, V_H_DiaryListDetails_BkgColour = "peachpuff", V_H_DiaryListDetails_Colour = "lightcyan" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 2, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CVC", V_H_DiaryListDetails_DateCreated = DTMonday, V_H_DiaryListDetails_Avail = 7, V_H_DiaryListDetails_BkgColour = "lightgray", V_H_DiaryListDetails_Colour = "red" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 3, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CC6", V_H_DiaryListDetails_DateCreated = DTMonday, V_H_DiaryListDetails_Avail = 1, V_H_DiaryListDetails_BkgColour = "pink", V_H_DiaryListDetails_Colour = "lightgray" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 4, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CV1", V_H_DiaryListDetails_DateCreated = DTMonday, V_H_DiaryListDetails_Avail = 4, V_H_DiaryListDetails_BkgColour = "red", V_H_DiaryListDetails_Colour = "lightcyan" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 5, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CVC", V_H_DiaryListDetails_DateCreated = DTTuesday, V_H_DiaryListDetails_Avail = 7, V_H_DiaryListDetails_BkgColour = "gray", V_H_DiaryListDetails_Colour = "pink" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 6, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CC6", V_H_DiaryListDetails_DateCreated = DTWednesday, V_H_DiaryListDetails_Avail = 1, V_H_DiaryListDetails_BkgColour = "pink", V_H_DiaryListDetails_Colour = "lightgray" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 1, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CV1", V_H_DiaryListDetails_DateCreated = DTThursday, V_H_DiaryListDetails_Avail = 5, V_H_DiaryListDetails_BkgColour = "orange", V_H_DiaryListDetails_Colour = "blue" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 6, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CVC", V_H_DiaryListDetails_DateCreated = DTFriday, V_H_DiaryListDetails_Avail = 7, V_H_DiaryListDetails_BkgColour = "blue", V_H_DiaryListDetails_Colour = "lawngreen" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 4, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CC6", V_H_DiaryListDetails_DateCreated = DTMonday, V_H_DiaryListDetails_Avail = 7, V_H_DiaryListDetails_BkgColour = "pink", V_H_DiaryListDetails_Colour = "lightblue" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 3, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CV1", V_H_DiaryListDetails_DateCreated = DTMonday, V_H_DiaryListDetails_Avail = 3, V_H_DiaryListDetails_BkgColour = "brown", V_H_DiaryListDetails_Colour = "lightcyan" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 2, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CVC", V_H_DiaryListDetails_DateCreated = DTTuesday, V_H_DiaryListDetails_Avail = 7, V_H_DiaryListDetails_BkgColour = "lightbrown", V_H_DiaryListDetails_Colour = "yellow" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 3, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CC6", V_H_DiaryListDetails_DateCreated = DTTuesday, V_H_DiaryListDetails_Avail = 1, V_H_DiaryListDetails_BkgColour = "silver", V_H_DiaryListDetails_Colour = "brown" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 2, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CV1", V_H_DiaryListDetails_DateCreated = DTTuesday, V_H_DiaryListDetails_Avail = 3, V_H_DiaryListDetails_BkgColour = "lightred", V_H_DiaryListDetails_Colour = "lightcyan" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 2, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CVC", V_H_DiaryListDetails_DateCreated = DTWednesday, V_H_DiaryListDetails_Avail = 7, V_H_DiaryListDetails_BkgColour = "lightgray", V_H_DiaryListDetails_Colour = "purple" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 3, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CC6", V_H_DiaryListDetails_DateCreated = DTWednesday, V_H_DiaryListDetails_Avail = 9, V_H_DiaryListDetails_BkgColour = "lightyellow", V_H_DiaryListDetails_Colour = "orange" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 1, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CV1", V_H_DiaryListDetails_DateCreated = DTThursday, V_H_DiaryListDetails_Avail = 3, V_H_DiaryListDetails_BkgColour = "peachpuff", V_H_DiaryListDetails_Colour = "lightcyan" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 2, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CVC", V_H_DiaryListDetails_DateCreated = DTThursday, V_H_DiaryListDetails_Avail = 8, V_H_DiaryListDetails_BkgColour = "lightgray", V_H_DiaryListDetails_Colour = "pink" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 5, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CC6", V_H_DiaryListDetails_DateCreated = DTThursday, V_H_DiaryListDetails_Avail = 9, V_H_DiaryListDetails_BkgColour = "lightviolet", V_H_DiaryListDetails_Colour = "lightgray" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 5, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CV1", V_H_DiaryListDetails_DateCreated = DTFriday, V_H_DiaryListDetails_Avail = 3, V_H_DiaryListDetails_BkgColour = "skyblue", V_H_DiaryListDetails_Colour = "silver" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 2, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CVC", V_H_DiaryListDetails_DateCreated = DTFriday, V_H_DiaryListDetails_Avail = 7, V_H_DiaryListDetails_BkgColour = "purple", V_H_DiaryListDetails_Colour = "lawngreen" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 6, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CC6", V_H_DiaryListDetails_DateCreated = DTMonday, V_H_DiaryListDetails_Avail = 1, V_H_DiaryListDetails_BkgColour = "pink", V_H_DiaryListDetails_Colour = "lightgray" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 6, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CV1", V_H_DiaryListDetails_DateCreated = DTWednesday, V_H_DiaryListDetails_Avail = 8, V_H_DiaryListDetails_BkgColour = "grren", V_H_DiaryListDetails_Colour = "lightcyan" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 2, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CVC", V_H_DiaryListDetails_DateCreated = DTWednesday, V_H_DiaryListDetails_Avail = 7, V_H_DiaryListDetails_BkgColour = "cream", V_H_DiaryListDetails_Colour = "orange" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 3, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CC6", V_H_DiaryListDetails_DateCreated = DTMonday, V_H_DiaryListDetails_Avail = 1, V_H_DiaryListDetails_BkgColour = "hazel", V_H_DiaryListDetails_Colour = "lightgray" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 1, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CV1", V_H_DiaryListDetails_DateCreated = DTFriday, V_H_DiaryListDetails_Avail = 3, V_H_DiaryListDetails_BkgColour = "peachpuff", V_H_DiaryListDetails_Colour = "lightcyan" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 2, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CVC", V_H_DiaryListDetails_DateCreated = DTFriday, V_H_DiaryListDetails_Avail = 7, V_H_DiaryListDetails_BkgColour = "lightgray", V_H_DiaryListDetails_Colour = "peachpuff" },
            //             new V_Home_DiaryListInfo { V_H_DiaryListDetails_id = 3, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CC6", V_H_DiaryListDetails_DateCreated = DTMonday, V_H_DiaryListDetails_Avail = 4, V_H_DiaryListDetails_BkgColour = "pink", V_H_DiaryListDetails_Colour = "lightgray" }
            //        );


            context.tblNonSittingDay.AddOrUpdate(c => c.NSD_ID,
                     new NonSittingDay { NSD_ID = 1, NSD_Reason = "Holiday", NSD_BackColor = "White", NSD_TextColor = "Black", NSD_Active = true, CreatedBy = "Arty", CreatedOn = DateTime.Parse("15/07/2014"),Deleted = false, NSD_Date = DateTime.Parse("03/08/2014"), ModifiedBy = "ArtyMod", ModifiedOn = DateTime.Now  },
                     new NonSittingDay { NSD_ID = 1, NSD_Reason = "Holiday2", NSD_BackColor = "Cream", NSD_TextColor = "Red", NSD_Active = true, CreatedBy = "Arty", CreatedOn = DateTime.Parse("15/07/2014"), Deleted = false, NSD_Date = DateTime.Parse("07/08/2014") , ModifiedBy = "ArtyMod", ModifiedOn = DateTime.Now }
                    
        //public DateTime NSD_Date { get; set; }      
        //public string NSD_BackColor { get; set; }     
        //public string NSD_TextColor { get; set;       
        //public string NSD_Reason { get; set; }
       
        //public bool NSD_Active { get; set; }
       
        //public string CreatedBy { get; set; }
        //public DateTime CreatedOn { get; set; }
      
        //public string ModifiedBy { get; set; }
        //public DateTime ModifiedOn { get; set; }
       
        //public bool Deleted { get; set; }
                 );

            context.tblDayNote.AddOrUpdate(d => d.DN_ID,
                    new DayNote { DN_ID = 1, DN_Note = "lkjc kd lsd lasda asdhgkasd", 
                        DN_Start_Date = DateTime.Parse("08/08/2014"), 
                        CreatedBy = "Arty", 
                        CreatedOn = DateTime.Parse("08/08/2014"), 
                        Deleted = false,
                        DN_BackColor = "Pink", 
                        DN_TextColor = "Black", 
                        IsActive = true, 
                        ModifiedBy = "Arty",
                        ModifiedOn = DateTime.Parse("08/08/2014"),
                        DN_End_Date = DateTime.Parse("08/08/2014"), 
                        DN_DiaryListCollection = new List<DN_ApplyToList> {
                                new DN_ApplyToList { 
                                            DNA_ID = 1,
                                            DN_ID  = 1,
                                            D_id   = 2,
                                            D_DiaryName = "VOCAT",    
                                            CreatedBy = "Arty1",
                                            CreatedOn = DateTime.Parse("03/08/2014"),    
                                            ModifiedBy = "Arty",
                                            ModifiedOn = DateTime.Parse("02/08/2014"),       
                                            Deleted = false
                                },
                                new DN_ApplyToList { 
                                            DNA_ID = 2,
                                            DN_ID  = 2,
                                            D_id   = 4,
                                            D_DiaryName = "Civil",    
                                            CreatedBy = "Arty2",
                                            CreatedOn = DateTime.Parse("04/08/2014"),    
                                            ModifiedBy = "Arty",
                                            ModifiedOn = DateTime.Parse("02/08/2014"),       
                                            Deleted = false
                                },
                                new DN_ApplyToList { 
                                            DNA_ID = 3,
                                            DN_ID  = 3,
                                            D_id   = 5,
                                            D_DiaryName = "Commitals",    
                                            CreatedBy = "Arty3",
                                            CreatedOn = DateTime.Parse("08/08/2014"),    
                                            ModifiedBy = "Arty",
                                            ModifiedOn = DateTime.Parse("02/08/2014"),       
                                            Deleted = false
                                }
                        }
                    });
        //public int DN_ID { get; set; }
        //public DateTime DN_Start_Date { get; set; }       
        //public DateTime DN_End_Date { get; set; }      
        //public string DN_BackColor { get; set; }      
        //public string DN_TextColor { get; set; }       
        //public string DN_Note { get; set; }      
        //public bool IsActive { get; set; }      
        //public string CreatedBy { get; set; }
        //public DateTime CreatedOn { get; set; }     
        //public string ModifiedBy { get; set; }
        //public DateTime ModifiedOn { get; set; }       
        //public bool Deleted { get; set; }      
        //public virtual ICollection<DN_ApplyToList> DN_DiaryListCollection { get; set; }


            context.tblCase.AddOrUpdate(e => e.C_ID,
                   new Case
                   {                 
                      D_id = 5,
                      C_D_DiaryName = "Commitals",
                      C_D_L_id = 1,
                      C_D_L_ShortName = "COMM",
                      C_Rules = "",
                      C_StartTime = Convert.ToDateTime("11:00AM"),
                      C_StartDate = Convert.ToDateTime("20-06-2014"),
                      C_EndDate = Convert.ToDateTime("28-06-2014"),
                      C_NoOfDays= 1,
                      C_Duration = "1.5",
                      C_Accused = "John sadsds",
                      C_Number = "A1000RT55",
                      C_Informant = "Graham",
                      C_MajorChange = "Theft",
                      C_Solicitor = "Mr Wand",
                      C_Location = "",
                      C_Category = "MMN",
                      C_Flags = new List<D_L_Flag> { new D_L_Flag {  
                                D_L_id = 1,
                                F_Name = "Flag1",
                                F_FilterType = "" ,
                                CreatedBy = "Arty",
                                CreatedOn =  DateTime.Parse("08/08/2014"),  
                                ModifiedBy = "Arty",
                                ModifiedOn =  DateTime.Parse("08/08/2014"),    
                                Deleted = false
                      }},
                      C_Notes = "Notes area",
                      C_JO_ID = 1,
                      C_JudicialOfficer = "Magistrate Lorry",
                      C_JO_Minutes = "",
                      CreatedBy = "Arty", 
                      CreatedOn = DateTime.Parse("02/08/2014"),
                      ModifiedBy = "Arty1",
                      ModifiedOn =  DateTime.Parse("02/08/2014")

                   },

                  new Case
                  {
                      D_id = 4,
                      C_D_DiaryName = "Civil",
                      C_D_L_id = 2,
                      C_D_L_ShortName = "CIV",
                      C_Rules = "",
                      C_StartTime = Convert.ToDateTime("11:00"),
                      C_StartDate = Convert.ToDateTime("20-06-2014"),
                      C_EndDate = Convert.ToDateTime("28-06-2014"),
                      C_NoOfDays= 1,
                      C_Duration = "2",
                      C_Accused = "Mr Person",
                      C_Number = "TYHM89I",
                      C_Informant = "Graham",
                      C_MajorChange = "Drug Pocession",
                      C_Solicitor = "Mr Groov",
                      C_Location = "",
                      C_Category = "MMN",
                      C_Flags = new List<D_L_Flag> { new D_L_Flag {  
                                D_L_id = 2,
                                F_Name = "Flag1",
                                F_FilterType = "" ,
                                CreatedBy = "Arty",
                                CreatedOn =  DateTime.Parse("08/08/2014"),  
                                ModifiedBy = "Arty",
                                ModifiedOn =  DateTime.Parse("08/08/2014"),    
                                Deleted = false
                      }},
                      C_Notes = "Notes area",
                      C_JO_ID = 1,
                      C_JudicialOfficer = "Magistrate Lorry",
                      C_JO_Minutes = "",
                      CreatedBy = "Arty", 
                      CreatedOn = DateTime.Parse("02/08/2014"),
                      ModifiedBy = "Arty1",
                      ModifiedOn =  DateTime.Parse("02/08/2014")
                  },

                  new Case
                  {                  
                      D_id = 2,
                      C_D_DiaryName = "VOCAT",
                      C_D_L_id = 3,
                      C_D_L_ShortName = "VCAT",
                      C_Rules = "",
                      C_StartTime = Convert.ToDateTime("11:00"),
                      C_StartDate = Convert.ToDateTime("20-06-2014"),
                      C_EndDate = Convert.ToDateTime("28-06-2014"),
                      C_NoOfDays= 1,
                      C_Duration = "1.5",
                      C_Accused = "Mr Person",
                      C_Number = "MMJJ**(",
                      C_Informant = "Graham",
                      C_MajorChange = "Hit And Run",
                      C_Solicitor = "Mr Cartman",
                      C_Location = "",
                      C_Category = "MMN",
                      C_Flags = new List<D_L_Flag> { new D_L_Flag {  
                                D_L_id = 3,
                                F_Name = "Flag3",
                                F_FilterType = "" ,
                                CreatedBy = "Arty",
                                CreatedOn =  DateTime.Parse("08/08/2014"),  
                                ModifiedBy = "Arty",
                                ModifiedOn =  DateTime.Parse("08/08/2014"),    
                                Deleted = false
                      }},
                      C_Notes = "Notes area",
                      C_JO_ID = 1,
                      C_JudicialOfficer = "Magistrate Frill",
                      C_JO_Minutes = "",
                      CreatedBy = "Arty", 
                      CreatedOn = DateTime.Parse("02/08/2014"),
                      ModifiedBy = "Arty1",
                      ModifiedOn =  DateTime.Parse("02/08/2014")

                  });    
  
        //public int    C_ID { get; set; }
        //public int    D_id { get; set; }
        //public string C_D_DiaryName { get; set; }
        //public int    C_D_L_id { get; set; }
        //public string C_D_L_ShortName { get; set; }
        //public string C_Rules { get; set; }
        //public DateTimC_StartDate { get; set; }
        //public DateTimC_EndDate { get; set; }
        //public int    C_NoOfDays { get; set; }
        //public DateTimC_Duration { get; set; }
        //public DateTimC_StartTime { get; set; }
        //public string C_Number { get; set; }
        //public string C_Accused { get; set; }
        //public string C_Informant { get; set; }
        //public string C_MajorChange { get; set; }
        //public string C_Solicitor { get; set; }
        //public string C_Location { get; set; }
        //public string C_Category { get; set; }
        //public string C_Flags { get; set; }
        //public string C_Notes { get; set; }
        //public int    C_JO_ID { get; set; }
        //public string C_JudicialOfficer { get; set; }
        //public string C_JO_Minutes { get; set; }
        //public string CreatedBy { get; set; }
        //public DateTimCreatedOn { get; set; }
        //public string ModifiedBy { get; set; }
        //public DateTimModifiedOn { get; set; }


        }
    }
}
