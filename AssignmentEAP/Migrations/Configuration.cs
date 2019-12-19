namespace AssignmentEAP.Migrations
{
    using AssignmentEAP.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AssignmentEAP.Models.MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AssignmentEAP.Models.MyDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            

            //var students = new List<Student>()
            //{
            //new Student()
            //{
            //    RollNumber = "T180702E",
            //    Student_Name = "Đoàn Văn Hậu",
            //    Avatar = "https://vpf.vn/wp-content/uploads/2018/12/Doan-Van-Hau.jpg",
            //    //Birthday =new DateTime(1999-04-12),
            //    Email = "Doanhau99@gmail.com",
            //    Phone = "0912025678",
            //    Class_Id = 1
            //},
            //new Student()
            //{
            //    RollNumber = "T180702E",
            //    Student_Name = "Phan Văn Đức",
            //    Avatar = "https://image1.thegioitre.vn/2017/10/24/duc-8.jpg",
            //    //Birthday =new DateTime(1996-02-02),
            //    Email = "PhanDuc99@gmail.com",
            //    Phone = "0912025678",
            //    Class_Id = 1
            //},
            //new Student()
            //{
            //    RollNumber = "T180702E",
            //    Student_Name = "Nguyễn Văn An",
            //    Avatar = "http://image.baoninhbinh.org.vn/Images/Uploaded/Share/2019/02/14/Untitled1-copy3.jpg",
            //    //Birthday =new DateTime(1999-05-06),
            //    Email = "Annguyen99@gmail.com",
            //    Phone = "0912345678",
            //    Class_Id = 1
            //},
            //new Student()
            //{
            //    RollNumber = "T1807033E",
            //    Student_Name = "Đặng Trần Tùng",
            //    Avatar = "http://imgs.vietnamnet.vn/Images/2016/08/13/12/20160813095706-thanh1.jpg",
            //    //Birthday = new DateTime(1995-02-03),
            //    Email = "Tungtran99@gmail.com",
            //    Phone = "0912345678",
            //    Class_Id = 1
            //},
            //new Student()
            //{
            //    RollNumber = "T180762E",
            //    Student_Name = "Nguyễn Thị Ngọc",
            //    Avatar = "https://we25.vn/media2018/Img_News/2019/08/11/nu-sinh-bat-ngo-duoc-ham-mo2_20190811002050.jpg",
            //    //Birthday =new DateTime(1999-07-03),
            //    Email = "Ngoc99@gmail.com",
            //    Phone = "099545678",
            //    Class_Id = 1
            //},
            //new Student()
            //{
            //    RollNumber = "T170502M",
            //    Student_Name = "Trần Văn Bảo",
            //    Avatar = "http://www.saovui.net/images/2017/09/nam-than-hoc-duong-noi-tieng-voi-goc-nghieng-dep-trai-nhu-tai-tu-hinh-anh-26378-439.jpg",
            //    //Birthday = new DateTime(1997-10-10),
            //    Email = "TranVanBao1010@gmail.com",
            //    Phone = "0345628926",
            //    Class_Id = 2
            //},
            //new Student()
            //{
            //    RollNumber = "T170508M",
            //    Student_Name = "Nguyễn Ngọc Lan",
            //    Avatar = "https://media.doisongvietnam.vn/u/rootimage/editor/2019/03/28/17/13/w825/5551553746412_9778.jpg",
            //    //Birthday = new DateTime(2000-03-04),
            //    Email = "Lanngoc2000@gmail.com",
            //    Phone = "0345628326",
            //    Class_Id = 2
            //},
            //new Student()
            //{
            //    RollNumber = "T170539M",
            //    Student_Name = "Trần Bảo Nguyên",
            //    Avatar = "https://znews-photo.zadn.vn/w660/Uploaded/kcwvouvs/2017_09_06/15027981_605712746283384_1108263185129405725_n.jpg",
            //    //Birthday = new DateTime(2000-12-12),
            //    Email = "TranBaoNguyen2000@gmail.com",
            //    Phone = "0984562836",
            //    Class_Id = 2
            //},
            //new Student()
            //{
            //    RollNumber = "T170539M",
            //    Student_Name = "Trần Bảo Trâm ",
            //    Avatar = "https://ttol.vietnamnetjsc.vn/images/2019/05/31/14/19/nu-sinh-anh-the-1.jpg",
            //    //Birthday = new DateTime(2000-02-01),
            //    Email = "Baotram0201@gmail.com",
            //    Phone = "0984653836",
            //    Class_Id = 2
            //},
            //new Student()
            //{
            //    RollNumber = "T190902A",
            //    Student_Name = "Trần Văn Trung",
            //    Avatar = "https://www.lofficiel.vn/wp-content/uploads/2019/09/30/71500552_521227168668393_8973120140946178048_n-800x800.jpg",
            //    //Birthday = new DateTime(1996-01-12),
            //    Email = "Trungtran2000@gmail.com",
            //    Phone = "098852836",
            //    Class_Id = 3
            //},
            //new Student()
            //{
            //    RollNumber = "T190939A",
            //    Student_Name = "Đặng Văn Lâm ",
            //    Avatar = "https://baohoahaudoanhnhan.vn/wp-content/uploads/2017/12/c6.jpg",
            //    //Birthday = new DateTime(2000-02-01),
            //    Email = "Danglam0201@gmail.com",
            //    Phone = "0984653836",
            //    Class_Id = 3
            //},
            //new Student()
            //{
            //    RollNumber = "T1909022A",
            //    Student_Name = "Nguyễn Quang Hải",
            //    Avatar = "https://bizweb.dktcdn.net/100/175/849/files/chup-anh-xin-visa-640003619-2096302183955875-7548803982232125440-o.jpg?v=1553234104024",
            //    //Birthday = new DateTime(1997-11-12),
            //    Email = "Quanghai97@gmail.com",
            //    Phone = "098899836",
            //    Class_Id = 3
            //},
            //new Student()
            //{
            //    RollNumber = "T190939A",
            //    Student_Name = "Nguyễn Duy Mạnh",
            //    Avatar = "http://kenh14cdn.com/2018/4/26/208002668078432393958852056770088485851649n-15247463661171351888022.jpg",
            //   // Birthday = new DateTime(2000-02-01),
            //    Email = "DuyManh@gmail.com",
            //    Phone = "098460536",
            //    Class_Id = 3
            //},
            //new Student()
            //{
            //    RollNumber = "T170706E",
            //    Student_Name = "Nguyễn Trọng Hùng",
            //    Avatar = "http://kenh14cdn.com/2018/4/26/208002668078432393958852056770088485851649n-15247463661171351888022.jpg",
            //    //Birthday = new DateTime(2000-02-01),
            //    Email = "Hung2000@gmail.com",
            //    Phone = "098460536",
            //    Class_Id = 4
            //},
            // };
            //var classes = new List<Class>()
            //{
            //    new Class()
            //    {
            //        Class_name = "T1807E",
            //    },
            //    new Class()
            //    {
            //        Class_name = "T1805M",
            //    },
            //    new Class()
            //    {
            //        Class_name = "T1909A",
            //    },
            //    new Class()
            //    {
            //        Class_name = "T1707E",
            //    },
            //};


            //context.Classes.AddRange(classes);
            //context.Students.AddRange(students);
            
            //context.SaveChanges();
        }
    }
}
