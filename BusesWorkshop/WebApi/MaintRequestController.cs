using BusesWorkshop.DAL.Bus;
using BusesWorkshop.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BusesWorkshop.WebApi
{
    public class MaintRequestController : ApiController
    {
        [HttpPost]
        public IHttpActionResult SaveMaintRequest(MaintRequestVM model)
        {
            using (WorkshopDataContext dcWorkShop = new WorkshopDataContext())
            {
                if (model != null)
                {
                    #region Save MaintRequest
                    if (model.maintRequest != null)
                    {
                        MaintRequest _MaintRequest = new MaintRequest()
                        {
                            PriorUrgent = model.maintRequest.PriorUrgent,
                            PriorHigh = model.maintRequest.PriorHigh,
                            PriorLow = model.maintRequest.PriorLow,
                            CompanyId = model.maintRequest.CompanyId,
                            LocationId = model.maintRequest.LocationId,
                            RequestedEmpId = model.EmpId,
                            RequestDate = model.maintRequest.RequestDate,
                            Notes = model.maintRequest.Notes,
                        };
                        dcWorkShop.MaintRequests.InsertOnSubmit(_MaintRequest);
                        dcWorkShop.SubmitChanges();
                        model.maintRequest.MaintReqId = dcWorkShop.MaintRequests.Where(
                            m => m.PriorUrgent == model.maintRequest.PriorUrgent
                            && m.PriorHigh == model.maintRequest.PriorHigh &&
                             m.PriorLow == model.maintRequest.PriorLow &&
                            m.CompanyId == model.maintRequest.CompanyId &&
                            m.LocationId == model.maintRequest.LocationId &&
                             m.RequestedEmpId == model.EmpId &&
                            m.RequestDate == model.maintRequest.RequestDate &&
                             m.Notes == model.maintRequest.Notes).OrderByDescending(m=>m.RequestDate ).Select(m=>m.MaintReqId).FirstOrDefault();
                       foreach (var i in model.maintReqDetailList)
                        {
                            i.MaintReqId = model.maintRequest.MaintReqId;
                        }
                       foreach(var i in model.maintReqPictureList)
                        {
                            i.MaintReqId = model.maintRequest.MaintReqId;
                        }
                    }
                    else
                        return BadRequest("عفوا لابد من ادخال اعمال الصيانة");
                    #endregion

                    #region save Works
                    if (model.maintReqDetailList != null)
                    {
                        if (model.maintReqDetailList.Count() > 0)
                        {
                            foreach (var item in model.maintReqDetailList)
                            {
                                MaintReqDetail _MaintReqDetail = new MaintReqDetail()
                                {
                                    MaintReqId = item.MaintReqId,
                                    MobileId = item.MobileId,
                                    WorkId = item.WorkId,
                                    PicDescription = item.PicDescription
                                };
                                dcWorkShop.MaintReqDetails.InsertOnSubmit(_MaintReqDetail);
                            }
                            dcWorkShop.SubmitChanges();
                        }
                        else
                            return BadRequest("عفوا لابد من ادخال تفاصيل اعمال الصيانة");
                    }
                    else
                        return BadRequest("عفوا لابد من ادخال تفاصيل اعمال الصيانة");
                    #endregion

                    #region SaveImages
                    if (model.maintReqDetailList != null)
                    {
                        if (model.maintReqDetailList.Count() > 0)
                        {

                            foreach (var item in model.maintReqPictureList)
                            {
                                MaintReqPicture _MaintReqPicture = new MaintReqPicture()
                                {
                                    MaintReqId = item.MaintReqId,
                                    PicturePath = item.PicturePath,
                                    Description = item.Description
                                };
                                dcWorkShop.MaintReqPictures.InsertOnSubmit(_MaintReqPicture);
                                dcWorkShop.SubmitChanges();
                            }
                        }
                    }
                }
                #endregion
            }
            return Ok("تم حفظ طلب الصيانة بنجاح");
        }
    }
}
