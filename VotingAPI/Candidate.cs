using System;

namespace VotingAPI
{
    public class Candidate
    {
        public string candidate { get; set; }
        public int age { get; set; }
        public int vote { get; set; }
        public double percent { get; set; }

        public Candidate(string _candidate, int _age, int _vote = 0)
        {
            this.candidate = _candidate;
            this.age = _age;
            this.vote = _vote;
        }

        public int getVote()
        {
            return this.vote;
        }

        public void setPercent(double _percent)
        {
            this.percent = _percent;
        }
    }
}
