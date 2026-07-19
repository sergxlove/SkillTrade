using SkillTrade.Core.Models;

namespace SkillTrade.MailService.Infrastructures
{
    public class BodyMails
    {
        public static string GetBody(MailType type, string code)
        {
            switch (type)
            {
                case MailType.None:
                    return string.Empty;
                case MailType.Restore:
                    return $@"
                        <!DOCTYPE html>
                        <html>
                        <head>
                            <meta charset='utf-8'>
                            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                            <title>SkillTrade - Код подтверждения</title>
                            <link href=""https://fonts.googleapis.com/css2?family=Inter:opsz,wght@14..32,300;14..32,400;14..32,500;14..32,600;14..32,700&display=swap"" rel=""stylesheet"">
                            <style>
                                * {{ margin:0; padding:0; box-sizing:border-box; }}
                                body {{
                                    font-family: 'Inter', sans-serif;
                                    background: #f0f4ff;
                                    color: #1a2332;
                                    padding: 20px;
                                    line-height: 1.6;
                                }}
                                .container {{
                                    max-width: 560px;
                                    margin: 0 auto;
                                    background: #ffffff;
                                    border-radius: 24px;
                                    padding: 40px 36px;
                                    box-shadow: 0 4px 24px rgba(59, 130, 246, 0.08);
                                    border: 1px solid #e8edf8;
                                }}
                                .header {{
                                    display: flex;
                                    align-items: center;
                                    gap: 10px;
                                    padding-bottom: 20px;
                                    border-bottom: 2px solid #eef3ff;
                                    margin-bottom: 28px;
                                }}
                                .logo {{
                                    display: flex;
                                    align-items: center;
                                    gap: 10px;
                                    font-size: 24px;
                                    font-weight: 700;
                                    background: linear-gradient(135deg, #7c3aed, #3b82f6);
                                    -webkit-background-clip: text;
                                    background-clip: text;
                                    color: transparent;
                                }}
                                .logo i {{ color: #3b82f6; font-size: 28px; }}

                                h1 {{ font-size: 26px; font-weight: 700; color: #1a2332; margin-bottom: 8px; }}
                                p {{ color: #4a5a7a; font-size: 15px; margin-bottom: 16px; }}
                                .code-block {{
                                    background: #f4f8ff;
                                    border: 2px dashed #3b82f6;
                                    border-radius: 16px;
                                    padding: 20px;
                                    text-align: center;
                                    margin: 20px 0;
                                }}
                                .code {{
                                    font-size: 40px;
                                    font-weight: 700;
                                    letter-spacing: 8px;
                                    color: #3b82f6;
                                    font-family: 'Inter', monospace;
                                }}
                                .card-warning {{
                                    background: #fffbeb;
                                    border: 1px solid #f59e0b;
                                    border-radius: 12px;
                                    padding: 14px 18px;
                                    margin: 16px 0;
                                    font-size: 14px;
                                }}
                                .footer {{
                                    text-align: center;
                                    padding-top: 24px;
                                    margin-top: 28px;
                                    border-top: 2px solid #eef3ff;
                                    font-size: 12px;
                                    color: #8899bb;
                                }}
                                .footer p {{ margin: 4px 0; font-size: 12px; color: #8899bb; }}
                                .footer a {{ color: #3b82f6; text-decoration: none; }}
                                @media (max-width: 480px) {{
                                    .container {{ padding: 24px 20px; }}
                                    .code {{ font-size: 30px; letter-spacing: 4px; }}
                                    h1 {{ font-size: 22px; }}
                                }}
                            </style>
                        </head>
                        <body>
                            <div class=""container"">
                                <div class=""header"">
                                    <div class=""logo""><i>🎓</i><span>SkillTrade</span></div>
                                </div>

                                <h1>🔐 Добро пожаловать!</h1>
                                <p>Мы получили запрос на сброс пароля для вашей учётной записи <strong>SkillTrade</strong>. Для подтверждения сброса используйте следующий код:</p>

                                <div class=""code-block"">
                                    <div class=""code"">{code}</div>
                                </div>

                                <p>Код действителен <strong>10 минут</strong>.</p>

                                <div class=""card-warning"">
                                    ⚠️ Если вы не запрашивали код, просто проигнорируйте это письмо.
                                </div>

                                <div class=""footer"">
                                    <p>© 2026 <strong>SkillTrade</strong> — Образовательная платформа</p>
                                    <p style=""margin-top:8px; font-size:11px; color:#aabbdd;"">Это автоматическое сообщение, не отвечайте на него.</p>
                                </div>
                            </div>
                        </body>
                        </html>
                        ";
                case MailType.Verify:
                    return $@"
                        <!DOCTYPE html>
                        <html>
                        <head>
                            <meta charset='utf-8'>
                            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                            <title>SkillTrade - Код подтверждения</title>
                            <link href=""https://fonts.googleapis.com/css2?family=Inter:opsz,wght@14..32,300;14..32,400;14..32,500;14..32,600;14..32,700&display=swap"" rel=""stylesheet"">
                            <style>
                                * {{ margin:0; padding:0; box-sizing:border-box; }}
                                body {{
                                    font-family: 'Inter', sans-serif;
                                    background: #f0f4ff;
                                    color: #1a2332;
                                    padding: 20px;
                                    line-height: 1.6;
                                }}
                                .container {{
                                    max-width: 560px;
                                    margin: 0 auto;
                                    background: #ffffff;
                                    border-radius: 24px;
                                    padding: 40px 36px;
                                    box-shadow: 0 4px 24px rgba(59, 130, 246, 0.08);
                                    border: 1px solid #e8edf8;
                                }}
                                .header {{
                                    display: flex;
                                    align-items: center;
                                    gap: 10px;
                                    padding-bottom: 20px;
                                    border-bottom: 2px solid #eef3ff;
                                    margin-bottom: 28px;
                                }}
                                .logo {{
                                    display: flex;
                                    align-items: center;
                                    gap: 10px;
                                    font-size: 24px;
                                    font-weight: 700;
                                    background: linear-gradient(135deg, #7c3aed, #3b82f6);
                                    -webkit-background-clip: text;
                                    background-clip: text;
                                    color: transparent;
                                }}
                                .logo i {{ color: #3b82f6; font-size: 28px; }}

                                h1 {{ font-size: 26px; font-weight: 700; color: #1a2332; margin-bottom: 8px; }}
                                p {{ color: #4a5a7a; font-size: 15px; margin-bottom: 16px; }}
                                .code-block {{
                                    background: #f4f8ff;
                                    border: 2px dashed #3b82f6;
                                    border-radius: 16px;
                                    padding: 20px;
                                    text-align: center;
                                    margin: 20px 0;
                                }}
                                .code {{
                                    font-size: 40px;
                                    font-weight: 700;
                                    letter-spacing: 8px;
                                    color: #3b82f6;
                                    font-family: 'Inter', monospace;
                                }}
                                .card-warning {{
                                    background: #fffbeb;
                                    border: 1px solid #f59e0b;
                                    border-radius: 12px;
                                    padding: 14px 18px;
                                    margin: 16px 0;
                                    font-size: 14px;
                                }}
                                .footer {{
                                    text-align: center;
                                    padding-top: 24px;
                                    margin-top: 28px;
                                    border-top: 2px solid #eef3ff;
                                    font-size: 12px;
                                    color: #8899bb;
                                }}
                                .footer p {{ margin: 4px 0; font-size: 12px; color: #8899bb; }}
                                .footer a {{ color: #3b82f6; text-decoration: none; }}
                                @media (max-width: 480px) {{
                                    .container {{ padding: 24px 20px; }}
                                    .code {{ font-size: 30px; letter-spacing: 4px; }}
                                    h1 {{ font-size: 22px; }}
                                }}
                            </style>
                        </head>
                        <body>
                            <div class=""container"">
                                <div class=""header"">
                                    <div class=""logo""><i>🎓</i><span>SkillTrade</span></div>
                                </div>

                                <h1>🔐 Добро пожаловать!</h1>
                                <p>Благодарим зв регистрацию в <strong>SkillTrade</strong>. Для подтверждения email используйте следующий код:</p>

                                <div class=""code-block"">
                                    <div class=""code"">{code}</div>
                                </div>

                                <p>Код действителен <strong>10 минут</strong>.</p>

                                <div class=""card-warning"">
                                    ⚠️ Если вы не запрашивали код, просто проигнорируйте это письмо.
                                </div>

                                <div class=""footer"">
                                    <p>© 2026 <strong>SkillTrade</strong> — Образовательная платформа</p>
                                    <p style=""margin-top:8px; font-size:11px; color:#aabbdd;"">Это автоматическое сообщение, не отвечайте на него.</p>
                                </div>
                            </div>
                        </body>
                        </html>
                        ";
                case MailType.Transact:
                    return $@"
                        <!DOCTYPE html>
                        <html>
                        <head>
                            <meta charset='utf-8'>
                            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                            <title>SkillTrade - Код подтверждения</title>
                            <link href=""https://fonts.googleapis.com/css2?family=Inter:opsz,wght@14..32,300;14..32,400;14..32,500;14..32,600;14..32,700&display=swap"" rel=""stylesheet"">
                            <style>
                                * {{ margin:0; padding:0; box-sizing:border-box; }}
                                body {{
                                    font-family: 'Inter', sans-serif;
                                    background: #f0f4ff;
                                    color: #1a2332;
                                    padding: 20px;
                                    line-height: 1.6;
                                }}
                                .container {{
                                    max-width: 560px;
                                    margin: 0 auto;
                                    background: #ffffff;
                                    border-radius: 24px;
                                    padding: 40px 36px;
                                    box-shadow: 0 4px 24px rgba(59, 130, 246, 0.08);
                                    border: 1px solid #e8edf8;
                                }}
                                .header {{
                                    display: flex;
                                    align-items: center;
                                    gap: 10px;
                                    padding-bottom: 20px;
                                    border-bottom: 2px solid #eef3ff;
                                    margin-bottom: 28px;
                                }}
                                .logo {{
                                    display: flex;
                                    align-items: center;
                                    gap: 10px;
                                    font-size: 24px;
                                    font-weight: 700;
                                    background: linear-gradient(135deg, #7c3aed, #3b82f6);
                                    -webkit-background-clip: text;
                                    background-clip: text;
                                    color: transparent;
                                }}
                                .logo i {{ color: #3b82f6; font-size: 28px; }}

                                h1 {{ font-size: 26px; font-weight: 700; color: #1a2332; margin-bottom: 8px; }}
                                p {{ color: #4a5a7a; font-size: 15px; margin-bottom: 16px; }}
                                .code-block {{
                                    background: #f4f8ff;
                                    border: 2px dashed #3b82f6;
                                    border-radius: 16px;
                                    padding: 20px;
                                    text-align: center;
                                    margin: 20px 0;
                                }}
                                .code {{
                                    font-size: 40px;
                                    font-weight: 700;
                                    letter-spacing: 8px;
                                    color: #3b82f6;
                                    font-family: 'Inter', monospace;
                                }}
                                .card-warning {{
                                    background: #fffbeb;
                                    border: 1px solid #f59e0b;
                                    border-radius: 12px;
                                    padding: 14px 18px;
                                    margin: 16px 0;
                                    font-size: 14px;
                                }}
                                .footer {{
                                    text-align: center;
                                    padding-top: 24px;
                                    margin-top: 28px;
                                    border-top: 2px solid #eef3ff;
                                    font-size: 12px;
                                    color: #8899bb;
                                }}
                                .footer p {{ margin: 4px 0; font-size: 12px; color: #8899bb; }}
                                .footer a {{ color: #3b82f6; text-decoration: none; }}
                                @media (max-width: 480px) {{
                                    .container {{ padding: 24px 20px; }}
                                    .code {{ font-size: 30px; letter-spacing: 4px; }}
                                    h1 {{ font-size: 22px; }}
                                }}
                            </style>
                        </head>
                        <body>
                            <div class=""container"">
                                <div class=""header"">
                                    <div class=""logo""><i>🎓</i><span>SkillTrade</span></div>
                                </div>

                                <h1>🔐 Добро пожаловать!</h1>
                                <p>Мы получили запрос на пополнение баланса вашей учётной записи <strong>SkillTrade</strong>. Для подтверждения оплаты используйте следующий код:</p>

                                <div class=""code-block"">
                                    <div class=""code"">{code}</div>
                                </div>

                                <p>Код действителен <strong>10 минут</strong>.</p>

                                <div class=""card-warning"">
                                    ⚠️ Если вы не запрашивали код, просто проигнорируйте это письмо.
                                </div>

                                <div class=""footer"">
                                    <p>© 2026 <strong>SkillTrade</strong> — Образовательная платформа</p>
                                    <p style=""margin-top:8px; font-size:11px; color:#aabbdd;"">Это автоматическое сообщение, не отвечайте на него.</p>
                                </div>
                            </div>
                        </body>
                        </html>
                        ";
                case MailType.Enter:
                    return $@"
                        <!DOCTYPE html>
                        <html>
                        <head>
                            <meta charset='utf-8'>
                            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                            <title>SkillTrade - Код подтверждения</title>
                            <link href=""https://fonts.googleapis.com/css2?family=Inter:opsz,wght@14..32,300;14..32,400;14..32,500;14..32,600;14..32,700&display=swap"" rel=""stylesheet"">
                            <style>
                                * {{ margin:0; padding:0; box-sizing:border-box; }}
                                body {{
                                    font-family: 'Inter', sans-serif;
                                    background: #f0f4ff;
                                    color: #1a2332;
                                    padding: 20px;
                                    line-height: 1.6;
                                }}
                                .container {{
                                    max-width: 560px;
                                    margin: 0 auto;
                                    background: #ffffff;
                                    border-radius: 24px;
                                    padding: 40px 36px;
                                    box-shadow: 0 4px 24px rgba(59, 130, 246, 0.08);
                                    border: 1px solid #e8edf8;
                                }}
                                .header {{
                                    display: flex;
                                    align-items: center;
                                    gap: 10px;
                                    padding-bottom: 20px;
                                    border-bottom: 2px solid #eef3ff;
                                    margin-bottom: 28px;
                                }}
                                .logo {{
                                    display: flex;
                                    align-items: center;
                                    gap: 10px;
                                    font-size: 24px;
                                    font-weight: 700;
                                    background: linear-gradient(135deg, #7c3aed, #3b82f6);
                                    -webkit-background-clip: text;
                                    background-clip: text;
                                    color: transparent;
                                }}
                                .logo i {{ color: #3b82f6; font-size: 28px; }}

                                h1 {{ font-size: 26px; font-weight: 700; color: #1a2332; margin-bottom: 8px; }}
                                p {{ color: #4a5a7a; font-size: 15px; margin-bottom: 16px; }}
                                .code-block {{
                                    background: #f4f8ff;
                                    border: 2px dashed #3b82f6;
                                    border-radius: 16px;
                                    padding: 20px;
                                    text-align: center;
                                    margin: 20px 0;
                                }}
                                .code {{
                                    font-size: 40px;
                                    font-weight: 700;
                                    letter-spacing: 8px;
                                    color: #3b82f6;
                                    font-family: 'Inter', monospace;
                                }}
                                .card-warning {{
                                    background: #fffbeb;
                                    border: 1px solid #f59e0b;
                                    border-radius: 12px;
                                    padding: 14px 18px;
                                    margin: 16px 0;
                                    font-size: 14px;
                                }}
                                .footer {{
                                    text-align: center;
                                    padding-top: 24px;
                                    margin-top: 28px;
                                    border-top: 2px solid #eef3ff;
                                    font-size: 12px;
                                    color: #8899bb;
                                }}
                                .footer p {{ margin: 4px 0; font-size: 12px; color: #8899bb; }}
                                .footer a {{ color: #3b82f6; text-decoration: none; }}
                                @media (max-width: 480px) {{
                                    .container {{ padding: 24px 20px; }}
                                    .code {{ font-size: 30px; letter-spacing: 4px; }}
                                    h1 {{ font-size: 22px; }}
                                }}
                            </style>
                        </head>
                        <body>
                            <div class=""container"">
                                <div class=""header"">
                                    <div class=""logo""><i>🎓</i><span>SkillTrade</span></div>
                                </div>

                                <h1>🔐 Код подтверждения</h1>
                                <p>Вы запросили вход в <strong>SkillTrade</strong>. Используйте следующий код:</p>

                                <div class=""code-block"">
                                    <div class=""code"">{code}</div>
                                </div>

                                <p>Код действителен <strong>10 минут</strong>.</p>

                                <div class=""card-warning"">
                                    ⚠️ Если вы не запрашивали вход, просто проигнорируйте это письмо.
                                </div>

                                <div class=""footer"">
                                    <p>© 2026 <strong>SkillTrade</strong> — Образовательная платформа</p>
                                    <p style=""margin-top:8px; font-size:11px; color:#aabbdd;"">Это автоматическое сообщение, не отвечайте на него.</p>
                                </div>
                            </div>
                        </body>
                        </html>
                        ";
                default:
                    return string.Empty;
            }
        }

        public static string GetSubject(MailType type)
        {
            switch (type)
            {
                case MailType.None:
                    return string.Empty;
                case MailType.Restore:
                    return "Восстановление доступа SkillTrade";
                case MailType.Verify:
                    return "Подтверждение адреса электронной почты";
                case MailType.Transact:
                    return "Подтверждение оплаты SkillTrade";
                case MailType.Enter:
                    return "Подтверждение входа SkillTrade";
                default:
                    return string.Empty;
            }
        }
    }
}
