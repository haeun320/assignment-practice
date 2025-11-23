using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatViewer.Model
{
    public class ChatLog
    {
        public DateTime DateTime { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// ChatLine을 받아 파싱 및 유효성 점검 후 저장
        /// </summary>
        /// <param name="chatLine"></param>
        public ChatLog(string chatLine)
        {

        }

        /// <summary>
        /// 라인을 DateTime, Sender, Message 파싱 후 저장
        /// </summary>
        /// <param name="line"></param>
        /// <returns>파싱 성공 여부</returns>
        private bool parseLine(string line)
        {
            // Error, 시스템 메시지 등은 예외 처리 후 false 반환
            return false;
        }
    }
}
