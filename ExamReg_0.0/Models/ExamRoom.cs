using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReg_0._0.Models
{
    public class ExamRoom
    {
        private int roomId;
        private int roomOrder;
        private String roomLocation;
        private int computerNumber;

        public int RoomId { get => roomId; set => roomId = value; }
        public int RoomOrder { get => roomOrder; set => roomOrder = value; }
        public string RoomLocation { get => roomLocation; set => roomLocation = value; }
        public int ComputerNumber { get => computerNumber; set => computerNumber = value; }
        public ExamRoom(int roomId, string roomLocation, int computerNumber, int roomOrder)
        {
            this.computerNumber = computerNumber;
            this.roomId = roomId;
            this.roomOrder = roomOrder;
            this.roomLocation = roomLocation;
        }
    }
}
