create proc UpdateStatus
as
	-- set any status that is pending to successful
	update MilestoneRequestPayers
		set PayerStatusId = 1
		where PayerStatusId = 2

	-- set any status that is pending to successful
	update MilestoneSystemRequestPayers
		set PayerStatusId = 1
		where PayerStatusId = 2

	-- set any milestone in addfunds status to active
	update milestones
		set MilestoneStatus = 2
		where milestonestatus = 1

	-- set any milestone in await admin approval status to set to available for freelancer withdrawal
	update milestones
		set MilestoneStatus = 4
		where milestonestatus = 8

	-- set any milestone in freelancerWithdrawal status to set to paid
	update milestones
		set MilestoneStatus = 5
		where milestonestatus = 9

