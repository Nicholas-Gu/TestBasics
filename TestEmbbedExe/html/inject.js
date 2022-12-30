var $root = null;
var $vm = null;
var shopId = null; // 店铺 id
var config = {}; // 智能助手配置
var replyMaxTime = 5 * 60 * 1000; // 最大回复时间
var replyMixTime = 1 * 60 * 1000; // 最小回复时间
var replyText = ""; // 兜底回复文本
var replyImages = []; // 兜底回复图片
var zrgTimeout = 1 * 60 * 1000; // 转人工轮询超时时间，超过这个时间不触发转人工逻辑

var interval = setInterval(() => {
  $root = document.getElementById("entry_pc");
  if ($root) {
    $vm = $root.__vue__;
    if ($vm.model) {
      shopId = parseInt($vm.model.pcModel.mine.sellerId);
      clearInterval(interval);
      getConfig();
    }
  }
}, 100);

/**
 * 发送图片
 * @param url 图片地址
 * @param userId 用户 id
 */
async function sendImage(url, userId) {
  // 发送请求参数给 content_script
  window.postMessage(
    { model: "chromeCRX", type: "sendImage", params: { url, userId } },
    "*"
  );
}

/**
 * 发送文本
 * @param text 文本内容
 * @param userId 用户 id
 */
function sendText(text, userId, session) {
  $vm.userViewModel.imService.sendText(text, userId, {
    receiver: session,
    mine: $vm.model.pcModel.mine
  });

  window.postMessage(
    {
      model: "chromeCRX",
      type: "sendText",
      params: {
        event: "send_text",
        session: session
      }
    },
    "*"
  );
}

/**
 * 获取客服助手配置
 */
function getConfig() {
  window.postMessage(
    { model: "chromeCRX", type: "getConfig", params: { shopId } },
    "*"
  );
}

function loopEvent() {
  setInterval(() => {
    // 过滤出未回复的会话
    const allSessions = $vm.model.pcModel.allSessions.filter(
      session => session.sessionViewType === 12
    );

    let allMessages = window.localStorage.getItem("xiaoyiCrxMessages") || "[]";
    if (allMessages) {
      allMessages = JSON.parse(allMessages);
    }

    allSessions.forEach(session => {
      // 监控转人工事件
      if (config.config.recept_switch === 1) {
        // 用户点击转人工按钮在前端不显示，只显示 [自动回复消息]
        if (session.lastMessage === "[自动回复消息]") {
          // 根据时间判断是否已经插入，如果已经插入则不处理了
          // 如果超过当前时间指定分钟也不插入了
          if (
            allMessages.findIndex(
              m => m.session.time === session.time.getTime()
            ) !== -1 ||
            new Date().getTime() - session.time.getTime() > zrgTimeout
          ) {
            return;
          }

          // 时间转换为时间戳
          const copySession = Object.assign({}, session);
          copySession.time = copySession.time.getTime();

          // 发送转人工事件
          window.postMessage(
            {
              model: "chromeCRX",
              type: "sendZRG",
              params: {
                department_id: shopId,
                event: "to_manual",
                session: copySession
              }
            },
            "*"
          );
        }
      }

      // 兜底回复
      if (config.config.reply_switch === 1) {
        // 会话接受的时间和当前时间差，如果大于最小回复时间则发送兜底话术
        const diff = Date.now() - session.time.valueOf();
        if (diff >= replyMixTime && diff < replyMaxTime) {
          const copySession = Object.assign({}, session);
          copySession.replyTime = config.config.reply_minute;
          sendText(replyText, session.userId, copySession);

          // 有图片的话要发送图片
          replyImages.forEach((image, index) => {
            setTimeout(() => {
              sendImage(image, session.userId);
            }, 500 * (index + 1));
          });
        }
      }
    });
  }, 2000);
}

// 接收 background.js 的回调，发送图片
window.addEventListener(
  "message",
  async e => {
    switch (e.data.type) {
      // 执行发送图片命令
      case "sendImageCallback":
        $vm.model.pcModel.sendImage(
          e.data.response.file,
          e.data.response.userId
        );
        break;

      case "getConfigCallback":
        config = e.data.response;

        if (config.config) {
          replyMixTime = config.config.reply_minute * 60 * 1000;
          replyText = config.config.reply_content[0].text;
          replyImages = config.config.reply_content[0].images;

          // 判断是否可用
          if (config.version_info.useable) {
            loopEvent();
          }
        }
        break;

      case "switchUser":
        $vm.model.pcModel.switchUser({
          ud: e.data.session.userId
        });
        break;
    }
  },
  false
);
