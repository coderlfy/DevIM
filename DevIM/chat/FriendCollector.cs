using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevIM.chat
{
    class FriendCollector
    {
        public static List<Friend> _Friends = 
            new List<Friend>();

        private static Friend _newFriend = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldfriend"></param>
        /// <returns></returns>
        private static bool matchCallback(
            Friend oldfriend)
        {
            return _newFriend._User.uid
                == oldfriend._User.uid;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newfriend"></param>
        /// <returns></returns>
        public static Friend FindFriend(
            Friend newfriend)
        {
            _newFriend = newfriend;
            return _Friends.Find(matchCallback);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="friend"></param>
        /// <returns></returns>
        public static bool Add(Friend friend)
        {
            if (FindFriend(friend) == null)
            {
                _Friends.Add(friend);
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="friend"></param>
        public static bool Remove(Friend friend)
        { 
            Friend find = FindFriend(friend);
            if (find != null)
            {
                _Friends.Remove(find);
                return true;
            }
            else
                return false;
        }
    }
}
