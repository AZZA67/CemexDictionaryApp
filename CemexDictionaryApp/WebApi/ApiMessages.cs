using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.WebApi
{
    public class ApiMessages
    {
        public static string Done = string.Format("Done");
        public static string EmptyObject = string.Format("خطأ فى البيانات المرسلة");

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
    }
}
