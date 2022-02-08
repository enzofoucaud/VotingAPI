using FluentAssertions;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using VotingAPI;

namespace VotingAPISpec.Steps
{
    [Binding]
    public sealed class VotingStepDefinitions
    {

        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef
        private readonly ScenarioContext _scenarioContext;
        private readonly Scrutin _scrutin = new Scrutin();

        private List<Candidate> _listCandidates;

        private string _resultString;
        private int _resultInt;

        public VotingStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _listCandidates = new List<Candidate>();
        }

        [Given(@"new candidates")]
        public void GivenNewCandidates(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                Candidate _candidate = new Candidate(row["Names"], int.Parse(row["Ages"]));
                _listCandidates.Add(_candidate);
            }
            _scrutin.initCandidates(_listCandidates);
        }

        [Given(@"new round")]
        public void GivenNewRound()
        {
            _scrutin.setRound();
        }

        [Then(@"candidates should be (.*)")]
        public void ThenCandidatesShouldBe(int result)
        {
            _resultInt = _scrutin.getNumberOfCandidates();
            this._resultInt.Should().Be(result);
        }

        [Then(@"ages should be (.*)")]
        public void ThenAgesShouldBeTrue(string result)
        {
            _resultString = _scrutin.verifyAges();
            this._resultString.Should().Be(result);
        }

        [Given(@"give ""(.*)"" vote to ""(.*)""")]
        public void GivenGiveVoteTo(int _vote, string _candidate)
        {
            _scrutin.putVote(_candidate, _vote);
        }

        [When(@"close voting")]
        public void WhenCloseVoting()
        {
            _scrutin.setStateOfVoting(false);
        }

        [When(@"the winner is ""(.*)""")]
        public void WhenTheWinnerIs(string result)
        {
            _resultString = _scrutin.getWinnerRound();
            //var cand = _scrutin.getCandidates();
            //foreach (var c in cand)
            //{
            //    Console.WriteLine("Le candidat {0} a un {1}% de votes avec {2}", c.candidate, c.percent, c.vote);
            //}
            //Console.WriteLine("Il y a {0} votes", _scrutin.getTotalVotes());
            this._resultString.Should().Be(result);
        }
    }
}
