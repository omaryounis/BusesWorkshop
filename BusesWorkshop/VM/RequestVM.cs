using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusesWorkshop.VM
{
    public class 
        RequestVM : IEquatable<RequestVM>
    {
        public string PhaseName { get; set; }
        public int LeftId { get; set; }
        public int RightId { get; set; }
        public int MaintReqId { get; set; }
        public DateTime RequestDate { get; set; }
        public string CompName { get; set; }
        public string UserName { get; set; }
        public int? UserId { get; set; }
        public int? phase_Step { get; set; }
        public bool? IsClosed { get; set; }
        public bool? IsAccepted { get; set; }
        public bool? IsRejected { get; set; }
        public int? Phase_Order { get; set; }
        public int? requestType { get; set; }
        public string requestTypes { get; set; }
        public TimeSpan time { get; set; }
        public TimeSpan timeOfNow { get; set; }
        public int defTime { get; set; }
        public int defDay { get; set; }
        public string defdateTime { get; set; }
        public string None
            {get;set;}
        public bool Equals(RequestVM req)
        {
            if (
                LeftId == req.LeftId 
               && RightId == req.RightId&&MaintReqId==req.MaintReqId&&
               RequestDate==req.RequestDate&&CompName==req.CompName&&
               UserName==req.UserName&&UserId==req.UserId&&
               phase_Step==req.phase_Step&&IsClosed==req.IsClosed&&IsAccepted==req.IsAccepted
               &&IsRejected==req.IsRejected && Phase_Order==req.Phase_Order
              && requestType==req.requestType&&time==req.time&&timeOfNow==req.timeOfNow
              &&defTime==req.defTime&&defTime==req.defTime&&defDay==req.defDay )
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            int hashLeftId = LeftId == 0 ? 0 : LeftId.GetHashCode();
            int hashRightId = RightId == 0 ? 0 : RightId.GetHashCode();
            int hashMaintReqId = MaintReqId == 0 ? 0 : MaintReqId.GetHashCode();
            int hasdrequestType = requestType == 0 ? 0 : MaintReqId.GetHashCode();
            int hashUserId = UserId == 0 ? 0 : UserId.GetHashCode();
            int hashphase_Step = LeftId == 0 ? 0 : phase_Step.GetHashCode();
            int hashRequestDate = RequestDate == null ? 0 : RequestDate.GetHashCode();
            int hashCompName = CompName == null ? 0 : CompName.GetHashCode();
            int hashUserName = UserName == null ? 0 : UserName.GetHashCode();
            int hashIsClosed = IsClosed == null ? 0 : IsClosed.GetHashCode();
            int hashIsAccepted = IsAccepted == null ? 0 : IsAccepted.GetHashCode();
            int hashIsRejected = IsRejected == null ? 0 : IsRejected.GetHashCode();
            int hashPhase_Order = Phase_Order == 0 ? 0 : Phase_Order.GetHashCode();

            return hashLeftId ^ hashRightId^hashMaintReqId^hashUserId^hashphase_Step;
        }

    }
}