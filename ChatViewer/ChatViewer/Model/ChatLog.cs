using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatViewer.Model
{
    public class ChatLog
    {
        public bool IsValid { get; set; }
        public DateTime Time { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// ChatLine을 받아 파싱 및 유효성 점검 후 저장
        /// </summary>
        /// <param name="chatLine"></param>
        public ChatLog(string chatLine)
        {
            IsValid = ParseLine(chatLine);
        }

        /// <summary>
        /// 라인을 DateTime, Sender, Message 파싱 후 저장
        /// </summary>
        /// <param name="line"></param>
        /// <returns>파싱 성공 여부</returns>
        private bool ParseLine(string line)
        {
            if (line == null)
            {
                return false;
            }

            // 시간 파싱
            char[] delimiter = ['[', ']'];
            string[] words = line.Split(delimiter);

            DateTime dateTime;

            if (!DateTime.TryParse(words[1], out dateTime))
            {
                return false;
            }

            Time = dateTime;

            // 발신자, 메시지 내용 파싱
            words = words[2].Split(':', 2);

            if (words.Length < 2)
            {
                return false;
            }

            // 발신자 확인
            string name = words[0].Trim();
            if (name == "시스템" || name == "system")
            {
                return false;
            }

            Sender = name;

            // 메시지 확인
            Message = words[1].Trim();

            return true;
        }
    }
}
