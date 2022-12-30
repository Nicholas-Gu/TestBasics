#pragma once
#include "IPlayerState.h"

class PlayerStatePause : public IPlayerState
{
public:
	PlayerStatePause(PlayerContext *playerContext);
	virtual void play();
	virtual void pause();
	virtual void stop();
};