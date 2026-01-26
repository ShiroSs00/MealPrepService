MealPrepService - 3 Business Workflows (Môn Học ASP.NET Core MVC)
Dự án web-only (.NET 8, EF Core 8, 3-layer) - Không QR thực, không delivery - Demo đặt menu healthy + tracking

🎯 3 Flows Chính 
Flow 1: User Onboarding & Nutrition Profile (Team 1-2)
text
1. REGISTER: Email/Phone/Password → OTP verify → Create User
2. PROFILE: Height/Weight/Age/Gender/Activity → Auto calc BMR/TDEE/Kcal 
3. PREFERENCES: Goal(Allergies/Cuisine/Budget/MealsPerDay) → Save NutritionProfile
4. DASHBOARD: Targets hiển thị + "Đặt menu tuần" CTA
Demo: Form wizard → Nutrition targets calculated → Profile saved

Flow 2: Subscription & AI Menu Generation (Team 2-3 - AI only)
text
1. SELECT PLAN: Basic(21 meals)/Premium(35 meals) × tuần 
2. AI GENERATE: OpenAI tạo menu match profile (calo/macros/no allergy)
3. REVIEW menu tuần → Mock payment → Create Order + QR code text
4. OUTPUT: PDF menu tuần + "Copy QR: MEAL-ORDER-1234"
Demo: AI button → JSON menu → Payment mock → QR text generated

Flow 3: Order Payment & Meal Review (Team 3-5 - Web only)
text
1. PAYMENT: Paste QR "MEAL-ORDER-1234" → Mock VNPay → OrderStatus=Paid
2. MEAL REVIEW: Dropdown chọn meal → Rate 1-5⭐ + Comment  
3. TRACKING: Dashboard % hoàn thành + Rating avg + Weekly chart
4. REPORT: Export Excel "Tuần 1: 71% - 4.3⭐ avg"
Demo: Paste QR text → Mock pay → Rate meals → Dashboard + Export

🏗️ 3-Layer Architecture
text
MealPrepService.Web (MVC Controllers/Views) 
   ↓ references
MealPrepService.BLL (Services: NutritionService, AIMenuService)  
   ↓ references  
MealPrepService.DAL (EF Core: User, NutritionProfile, Order, MealReview)
📋 Core Entities
text
User → NutritionProfile(1:1) → Order(1:N) → MealReview(N)
🛠️ Tech Stack (Môn học friendly)
Backend: ASP.NET Core MVC (.NET 8) + EF Core 8 + SQL Server Local

AI: OpenAI API (Flow 2 menu generation - free tier OK)

Frontend: Razor Views + Bootstrap 5 + Chart.js

Payment: Mock button (không tích hợp thật)

QR: Text input "MEAL-ORDER-1234" (demo)

📊 Demo Checklist
 Flow 1: Register → Profile calc → Dashboard

 Flow 2: AI menu generate → Mock payment → QR text

 Flow 3: QR payment → Rate meals → Progress chart + Export Excel

 3-layer: Controller → Service → Repository → DB

🚀 Setup (5 phút)
bash
# Restore + Migration
dotnet restore
dotnet ef migrations add InitialCreate --project DAL --startup-project Web
dotnet ef database update --project DAL --startup-project Web

# Run
dotnet run --project Web

