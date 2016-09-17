using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class PlanLogic : BusinessLogic
    {
        private PlanAdapter _PlanData;  
        public PlanAdapter PlanData
        {
            get{ return _PlanData; }
            set { _PlanData = value; }
        } 
        public PlanLogic()
        {
            PlanData = new PlanAdapter();
        }





        public List<Plan> GetAll()
        {
            try
            {
                return PlanData.GetAll();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }
        public Plan GetOne(int id) 
        {
            try
            {
                return PlanData.GetOne(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public void Save(Plan plan)
        {
            PlanData.Save(plan);
        }
        public void Delete(int id)
        {
            try
            {
                PlanData.Delete(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        



    }
}
