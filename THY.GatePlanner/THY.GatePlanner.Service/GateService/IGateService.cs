﻿using System;
using THY.GatePlanner.Model.Requests;
using THY.GatePlanner.Model.Responses;

namespace THY.GatePlanner.Service.GateService
{
	public interface IGateService
	{
		Task<List<GetGatesResponse>> GetGatesAsync(GetGatesRequest? request);
	}
}

