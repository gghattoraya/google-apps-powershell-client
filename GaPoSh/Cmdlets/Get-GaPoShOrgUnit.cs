﻿using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GaPoShOrgUnit")]
    public class GetGaPoShOrgUnit : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = false)]
        public string UserId;

        [Parameter(Mandatory = false)]
        public bool All;

        [Parameter(Mandatory = false)]
        public string OrgUnitPath;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            var service = request.DirectoryService.Orgunits.List("my_customer");

            //Query Parameters

            service.Type = All
                               ? OrgunitsResource.ListRequest.TypeEnum.All
                               : OrgunitsResource.ListRequest.TypeEnum.Children;

            //Query by OrgUnit
            service.OrgUnitPath = All ? null : OrgUnitPath;

            var orgUnits = service.Execute();

            if (orgUnits == null) return;

            WriteObject(orgUnits);
        }
    }
}