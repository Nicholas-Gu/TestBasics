#pragma once
#include "IPlayerStatet.h"

class PlayerStateStop : public IPlayerState
{
public:
	PlayerStateStop(PlayerContext *playerContext);
	virtual void play();
	virtual void pause();
	virtual void stop();
};