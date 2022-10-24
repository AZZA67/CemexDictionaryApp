using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Core
{
    public class Messages
    {
        public static string Done = string.Format("Done");
        public static string EmptyObject = string.Format("خطأ فى البيانات المرسلة");
        public static string EmptyList = string.Format("لا توجد بيانات");

        //News
        public static string EmptyNewsList = string.Format("لا يوجد أخبار");

        //Products
        public static string EmptyProductList = string.Format("لا توجد منتجات");

        //User
        public static string MobileExist = string.Format("رقم الهاتف مسجل من قبل");
        public static string MobileNotExist = string.Format("رقم الهاتف غير مسجل");
        public static string WrongPassword = string.Format("خطأ فى كلمة المرور");
        public static string ConfirmRegistration = string.Format("تم تسجيل الحساب بنجاح");
        public static string RegistrationError = string.Format("خطأ اثناء تسجيل الحساب");
        public static string UserNotExist = string.Format("العميل غير موجود");


        //Questions
        public static string QuestionPosted = string.Format("تم تسجيل السؤال بنجاح");
        public static string EmptySearchText = string.Format("خطأ لا يوجد نص للبحث");
        public static string CustomerEmptyQuestons = string.Format("لا توجد أسئلة للعميل");

        //Notifcations
        public static string NewsTitle = string.Format("تم إضافة خبر جديد");
        public static string ProductTitle = string.Format("تم إضافة منتج جديد");
        public static string QuestionTitle = string.Format("تم إضافة سؤال جديد");

        public static string NewsMessage = string.Format("تم إضافة خبر جديد");
        public static string ProductMessage = string.Format("تم إضافة منتج جديد");
        public static string QuestionMessage = string.Format("تم إضافة سؤال جديد");
    }
}
