using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotingAPI
{
    public class Scrutin
    {
        public List<Candidate> candidates;
        public bool state = true;
        public int round = 0;

        public void initCandidates(List<Candidate> _candidates)
        {
            candidates = _candidates;
        }

        public List<Candidate> getCandidates()
        {
            return candidates;
        }

        public int getNumberOfCandidates()
        {
            return candidates.Count;
        }

        public void setStateOfVoting(bool _state)
        {
            state = _state;
        }

        public bool getStateOfVoting()
        {
            return state;
        }

        public string verifyAges()
        {
            foreach(var candidate in candidates)
            {
                if (candidate.age < 18)
                {
                    return "Error: " + candidate.candidate + " ne peut pas participer car il a " + candidate.age;
                }
            }
            return "Tout le monde peut participer";
        }

        public int getRound()
        {
            return round;
        }

        public void setRound()
        {
            round++;
        }

        public void putVote(string _candidate, int _vote)
        {
            foreach(var candidate in candidates)
            {
                if (_candidate == candidate.candidate){candidate.vote = _vote; return;}
            }
        }

        public int getTotalVotes()
        {
            int _votes = 0;
            foreach (var candidate in candidates)
            {
                _votes = _votes + candidate.vote;
            }
            return _votes;
        }

        public void calculatePercentByCandidate()
        {
            var _totalVotes = getTotalVotes();
            foreach (var candidate in candidates)
            {
                var result = ((double)candidate.getVote() / (double)_totalVotes) * 100;
                candidate.setPercent(result);
            }
        }

        public void setTwoHighVotes()
        {
            candidates.OrderByDescending(x => x.percent);
            var cdt1 = candidates.ElementAt(0);
            var cdt2 = candidates.ElementAt(1);
            var cdt3 = candidates.ElementAt(2);


            if (cdt2.percent != cdt3.percent)
            {
                candidates.RemoveRange(2, candidates.Count - 2);
            } else
            {
                List<Candidate> newListCandidate = new List<Candidate>();
                if(cdt2.age >= cdt3.age)
                {
                    newListCandidate.Add(cdt1);
                    newListCandidate.Add(cdt2);
                    candidates = newListCandidate;
                } else
                {
                    newListCandidate.Add(cdt1);
                    newListCandidate.Add(cdt3);
                    candidates = newListCandidate;
                }
            }
        }

        public string getWinnerRound()
        {
            if (getStateOfVoting() != true)
            {
                if (round == 1)
                {
                    calculatePercentByCandidate();
                    var i = 0;
                    foreach (var candidate in candidates)
                    {
                        if (candidate.percent > 50)
                        {
                            return candidates[i].candidate;
                        }
                        i++;
                    }
                    setTwoHighVotes();
                    return "Aucuns vainqueurs sur le premier tour, tour suivant";
                } 
                else if (round == 2)
                {
                    calculatePercentByCandidate();
                    var i = 0;
                    foreach (var candidate in candidates)
                    {
                        if (candidate.percent > 50)
                        {
                            return candidates[i].candidate;
                        }
                        i++;
                    }
                    return "Les candidats sont à égalités. Stop!";
                }
                else
                {
                    return "Error: Impossible de faire plus de round";
                }
            } 
            else
            {
                return "Error: Le vote n'est pas clos";
            }
            return "Contact admin";
        }
    }
}
