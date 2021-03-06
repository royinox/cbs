/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2017-2018 The International Federation of Red Cross and Red Crescent Societies. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using Dolittle.ReadModels;

namespace Read.ProjectFeatures
{
    public class ProjectHealthRiskVersion : IReadModel
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }
        public ProjectHealthRisk HealthRisk { get; set; }
        public DateTimeOffset EffectiveFromTime { get; set; }
    }
}