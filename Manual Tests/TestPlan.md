# Test Plan – SauceDemo QA Portfolio Project

## 1. Introduction
This test plan defines the testing approach for the **SauceDemo** web application (https://www.saucedemo.com).  
The goal is to validate key user journeys such as login, shopping cart, and checkout.

---

## 2. Scope
**In scope:**
- User login (valid and invalid users)
- Product browsing
- Adding/removing items from cart
- Checkout process (complete/blocked)
- Logout

**Out of scope:**
- Performance/load testing
- Backend/database verification


---

## 3. Test Strategy
- **Testing type:** Manual functional testing  
- **Test design:** Based on requirements observed from the UI  
- **Test levels:** System testing (end-to-end)  
- **Browsers:** Chrome (latest) 
- **Defect tracking:** Documented in Markdown files / Jira

---

## 4. Test Environment
- URL: https://www.saucedemo.com
- Browser: Chrome 122+ or Edge 122+
- Test accounts: 
  - `standard_user`
  - `locked_out_user`
  - `problem_user`
  - `performance_glitch_user`

---

## 5. Entry Criteria
- Test cases are prepared and reviewed  
- Access to SauceDemo environment is available  

---

## 6. Exit Criteria
- All planned test cases executed  
- Critical defects resolved or documented  
- Summary report prepared  

---

## 7. Deliverables
- Test Plan (`TestPlan.md`)  
- Test Cases (`TestCases.md`)  

---

## 8. Roles & Responsibilities
- **Tester:** [Obadah Zitawi] 

---

## 9. Risks & Mitigation
- Site availability issues → Retry later  
- Data resets on demo site → Use fresh sessions for testing  

---

## 10. Schedule
- Test design: 1 day  
- Test execution: 2–3 days  

- Documentation: 1 day

